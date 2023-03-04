using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

/*
Main class that defines Player's behaviour
*/
public class PlayerController : MonoBehaviourPunCallbacks, IDamageable, IPunObservable
{
    public static PlayerController Instance;
    public bool gamePauseToMenu = true;
    public Apple appleScript;

    // UI
    [SerializeField]
    private Image fruitBarImage;

    [SerializeField]
    private GameObject _ui_fruitbar;

    [SerializeField]
    private GameObject ui;

    //Movement variables
    public float moveSpeed,
        gravityModifier,
        jumpPower,
        stepSpeed = 1f;
    public CharacterController charCon;

    private Vector3 moveInput;
    public Transform camTransform;

    //Mouse variables
    public float mouseSensitivity;
    public bool binvertX;
    public bool binvertY;
    private bool bcanJump,
        bdoubleJump;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    //Gun Network Rotation Variables
    private int rotationOffset = 0;
    private Quaternion gunPos;
    private Vector3 difference;

    //ShootPull variables
    public Animator anim;
    public GameObject bullet;
    public Transform firePoint,
        pullPoint;
    public float pullDistance,
        pullSpeed;
    private GameObject fruitIpulled;
    private Rigidbody fruitRB;
    private Vector3 _velocity = Vector3.zero;

    //private int _maxPlayerFruits, _currentPlayerFruits;
    private const int _maxPlayerFruits = 100;
    private int _currentPlayerFruits;
    private int fruitShot = 1;
    private int _fruitTake = 4;
    RaycastHit hit;
    public GameObject fruitBulletPrefab;
    private PhotonView PV;
    private PlayerManager _playerManager;
    public GameObject bulletImpactPrefab;

    //[SerializeField] Image fruitBarImage;
    [SerializeField]
    private Item[] items;

    private int _itemIndex;
    private int _previousItemIndex = -1;

    //********************************************************************************************************

    // Runs before Start
    // Accesses Photon system and Player Manager class
    private void Awake()
    {
        Instance = this;
        PV = GetComponent<PhotonView>();
        _playerManager = PhotonView
            .Find((int)PV.InstantiationData[0])
            .GetComponent<PlayerManager>();
    }

    // Start is called before the first frame update
    // Accesses Apple fruit game object class
    void Start()
    {
        appleScript = GameObject.FindGameObjectWithTag("Fruit").GetComponent<Apple>();
        // Weapon Equipment
        if (PV.IsMine)
        {
            EquipItem(0);
        }
        else
        {
            Debug.Log("Destroying Camera PV is not mine");
            Destroy(GetComponentInChildren<Camera>().gameObject);
            //Destroy(ui);
            Destroy(_ui_fruitbar);
        }
    }

    // Update is called once per frame
    // Runs Player's movement, Shooting and Player's Jet's animation
    void Update()
    {
        if (!PV.IsMine)
        {
            //SmoothNetMovement();
            return;
        }

        Jets();
        Movement();
        ShootPull();
        OutOfMap();

        // Gun 2
        for (int i = 0; i < items.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                EquipItem(i);
                break;
            }
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            if (_itemIndex >= items.Length - 1)
            {
                EquipItem(0);
            }
            else
            {
                EquipItem(_itemIndex + 1);
            }
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            if (_itemIndex <= 0)
            {
                EquipItem((items.Length - 1));
            }
            else
            {
                EquipItem(_itemIndex - 1);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            items[_itemIndex].Use();
        }
    }

    // Fixed Updated is called every fixed frame-rate frame
    // Checks of current Photon view is local
    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;
    }

    //***********************************************************************************************************



    // Movement
    void Movement()
    {
        if (gamePauseToMenu == true)
        {
            float yStore = moveInput.y;

            Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
            Vector3 horzMove = transform.right * Input.GetAxis("Horizontal");
            //moveInput = (horzMove + vertMove) * moveSpeed; // this smart ass code did not work
            moveInput = horzMove + vertMove;
            moveInput.Normalize();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveInput = moveInput * stepSpeed;
            }
            else
            {
                moveInput = moveInput * moveSpeed;
            }

            moveInput.y = yStore;
            moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

            if (charCon.isGrounded)
            {
                moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
            }

            // Jumping
            bcanJump =
                Physics.OverlapSphere(groundCheckPoint.position, 0.25f, whatIsGround).Length > 0;

            if (bcanJump)
            {
                bdoubleJump = true;
            }

            if (Input.GetKeyDown(KeyCode.Space) && bcanJump)
            {
                moveInput.y = jumpPower;
                bdoubleJump = true;
            }
            else if (bdoubleJump && Input.GetKeyDown(KeyCode.Space))
            {
                moveInput.y = jumpPower;
                bdoubleJump = false;
            }

            // Character movement and rotation
            charCon.Move(moveInput * Time.deltaTime);

            //Control camera rotation
            Vector2 mouseInput =
                new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"))
                * mouseSensitivity;
            if (binvertX)
            {
                mouseInput.x = -mouseInput.x;
            }

            if (binvertY)
            {
                mouseInput.y = -mouseInput.y;
            }
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y + mouseInput.x,
                transform.rotation.eulerAngles.z
            );
            camTransform.rotation = Quaternion.Euler(
                camTransform.rotation.eulerAngles.x - mouseInput.y,
                camTransform.rotation.eulerAngles.y,
                camTransform.rotation.eulerAngles.z
            );

            // camera movement while running or slow walking
            anim.SetFloat("moveSpeed", moveInput.magnitude);
            anim.SetBool("onGround", bcanJump);
            anim.SetBool("onWalking", Input.GetKey(KeyCode.LeftShift));
        }
    }

    //---------------------------------Weapon Equipment Start ---------------
    void EquipItem(int _index)
    {
        if (_index == _previousItemIndex)
            return;

        _itemIndex = _index;
        items[_itemIndex].itemGameObject.SetActive(true);
        if (_previousItemIndex != -1)
        {
            items[_previousItemIndex].itemGameObject.SetActive(false);
        }
        _previousItemIndex = _itemIndex;

        if (PV.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("_itemIndex", _itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!PV.IsMine && targetPlayer == PV.Owner)
        {
            EquipItem((int)changedProps["_itemIndex"]);
        }
    }

    //---------------------------------Weapon Equipment End ---------------

    // Shoot/Pull
    void ShootPull()
    {
        if (gamePauseToMenu == true)
        {
            //Handle Shooting
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }

            // Handle Pulling
            if (Input.GetMouseButton(1))
            {
                Pull();
            }
        }
    }

    private void Shoot()
    {
        if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, 50f))
        {
            if (Vector3.Distance(camTransform.position, hit.point) > 2f)
            {
                firePoint.LookAt(hit.point);
            }
        }
        else
        {
            firePoint.LookAt(camTransform.position + (camTransform.forward * 30f));
        }

        if (PlayerFruitController.instance.currentFruits > 0)
        {
            fireFruitBullet();

            PlayerFruitController.instance.LoseFruits(fruitShot);
        }
    }

    private void Pull()
    {
        RaycastHit hit;
        if (fruitIpulled == null)
        {
            if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, pullDistance))
            {
                pullPoint.LookAt(hit.point);
                if (hit.collider.gameObject.CompareTag("Fruit"))
                {
                    Debug.Log("Hitting Fruit Tag");
                    fruitIpulled = hit.transform.gameObject;
                    hit.rigidbody.velocity = Vector3.zero;
                    fruitIpulled.GetPhotonView().RequestOwnership();
                    fruitIpulled
                        .GetComponent<Transform>()
                        .Find("CaptureField")
                        .GetComponent<Transform>()
                        .gameObject.SetActive(true);
                    fruitIpulled.transform.position = Vector3.SmoothDamp(
                        fruitIpulled.transform.position,
                        pullPoint.transform.position,
                        ref _velocity,
                        pullSpeed * Time.deltaTime
                    );
                    fruitIpulled = null;
                    hit.rigidbody.useGravity = true;
                    hit.rigidbody.isKinematic = false;
                }
            }
        }
    }

    IEnumerator RapidFireSlower()
    {
        yield return new WaitForSeconds(20f);
    }

    //GUN ROTATION---------------------------------------------------------------------------------------------------
    private void RotateGun()
    {
        difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);
    }

    private void SmoothNetMovement()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, gunPos, Time.deltaTime * 8);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.rotation);
        }
        else
        {
            gunPos = (Quaternion)stream.ReceiveNext();
        }
    }

    // Shoot/Pull End


    // Jets
    private void Jets()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
        {
            transform.GetChild(5).gameObject.SetActive(false);
            transform.GetChild(4).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(5).gameObject.SetActive(false);
            transform.GetChild(4).gameObject.SetActive(false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.GetChild(4).gameObject.SetActive(false);
            transform.GetChild(5).gameObject.SetActive(true);
        }

        if (
            Input.GetKeyDown(KeyCode.Space)
            || Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.A)
            || Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.D)
            || Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.W)
        )
        {
            PlayerJetsSync();
        }
    }

    public void TakeFruit(int takeFruit)
    {
        PV.RPC("RPC_TakeFruit", RpcTarget.All, takeFruit);
    }

    [PunRPC]
    void RPC_TakeFruit(int takeFruit)
    {
        if (!PV.IsMine)
            return;

        PlayerFruitController.instance.currentFruits -= takeFruit;

        if (PlayerFruitController.instance.currentFruits <= 0)
        {
            //GameOver();
        }
    }

    void GameOver()
    {
        _playerManager.GameOver();
    }

    void OutOfMap()
    {
        if (transform.position.y < -10f)
        {
            GameOver(); // Game Over if off the level area
        }
    }

    [PunRPC]
    void RPC_Shoot(Vector3 hitPosition, Vector3 hitNoraml)
    {
        Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.3f);
        if (colliders.Length != 0)
        {
            GameObject bulletImpactobj = Instantiate(
                bulletImpactPrefab,
                hitPosition + hitNoraml * 0.0001f,
                Quaternion.LookRotation(hitNoraml, Vector3.up)
                    * bulletImpactPrefab.transform.rotation
            );
            Destroy(bulletImpactobj, 1f);
            bulletImpactobj.transform.SetParent(colliders[0].transform);
        }
    }

    public void fireFruitBullet()
    {
        PV.RPC("RPC_fireFruitBullet", RpcTarget.All);
    }

    [PunRPC]
    void RPC_fireFruitBullet()
    {
        GameObject fruitBullet = Instantiate(
            fruitBulletPrefab,
            firePoint.position,
            firePoint.rotation
        );
    }

    //Player Jets Sync------------------------------------------------------------------------------------------------------
    public void PlayerJetsSync()
    {
        PV.RPC("RPC_PlayerJetsRoutine", RpcTarget.All);
    }

    [PunRPC]
    void RPC_PlayerJetsRoutine()
    {
        StartCoroutine(DoubleJetRoutine());
        IEnumerator DoubleJetRoutine()
        {
            transform
                .GetChild(3)
                .GetComponent<Transform>()
                .GetComponent<ParticleSystem>()
                .startLifetime = 0.6f;
            Debug.Log("Particles boosted");
            yield return new WaitForSeconds(0.25f);
            transform
                .GetChild(3)
                .GetComponent<Transform>()
                .GetComponent<ParticleSystem>()
                .startLifetime = 0.1f;
        }
    }
}
