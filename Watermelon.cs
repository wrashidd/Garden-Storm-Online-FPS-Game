using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.UIElements;
using Photon.Pun;
using UnityEngine;

public class Watermelon: MonoBehaviourPun
{
    private PhotonView PV;
    private PlayerController _playerController;
    private int _addFruit = 6;
    [SerializeField] private GameObject watermelon;
    public GameObject captureField;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("Pull Point"))
        {
            // Play side puff animations from the muzzle
            //PlayerController.Instance.MuzzleCloudParticle();
            PlayerFruitController.instance.AddFruits(_addFruit);
           // PhotonNetwork.Destroy(this.gameObject);
           // PhotonNetwork.Destroy(appleParent.gameObject);
           StartCoroutine(DestroyFruitWithDelay());
            Debug.Log("Destroyed the pulled object");
          
        }
        
        if (other.gameObject.CompareTag("MFruitReducer"))
        {
           watermelon.GetComponent<Transform>().localScale = new Vector3(0.2f, 0.2f,0.2f);
           captureField.GetComponent<Transform>().localScale = new Vector3(0.001f, 0.001f, 0.001f);
           
          
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MFruitReducer"))
        {
            watermelon.GetComponent<Transform>().localScale = new Vector3(1f,1f, 1f);
           captureField.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void CaptureFieldActivator(int Activate)
    {
        switch (Activate)
        {
            case 0:
                Debug.Log("CaputureFieild");
                captureField.SetActive(true);
                break;
            case 1 :
                captureField.SetActive(false);
                break;
        }
    }

    IEnumerator DestroyFruitWithDelay()
    {
        yield return new WaitForSeconds(0.1f);
        PhotonNetwork.Destroy(this.gameObject);
    }
}
