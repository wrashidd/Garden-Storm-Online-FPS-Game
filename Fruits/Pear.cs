using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.UIElements;
using Photon.Pun;
using UnityEngine;

/*
This class is responsible for Pear fruit game object logic
*/
public class Pear : MonoBehaviourPun
{
    private PhotonView PV;
    private PlayerController _playerController;
    private int _addFruit = 3;

    [SerializeField]
    private GameObject apple;
    public GameObject captureField;

    // Start is called before the first frame update
    // Accesses main Photon networking system class
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    // Triggers Pear's pulling, rescaling and destruction sequences
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pull Point"))
        {
            PlayerFruitController.instance.AddFruits(_addFruit);
            StartCoroutine(DestroyFruitWithDelay());
            Debug.Log("Destroyed the pulled object");
        }

        if (other.gameObject.CompareTag("MFruitReducer"))
        {
            apple.GetComponent<Transform>().localScale = new Vector3(0.75f, 0.75f, 0.75f);
            captureField.GetComponent<Transform>().localScale = new Vector3(0.001f, 0.001f, 0.001f);
        }
    }

    // Triggers Pear's rescaling sequence
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MFruitReducer"))
        {
            apple.GetComponent<Transform>().localScale = new Vector3(2.5f, 2.5f, 2.5f);
            captureField.GetComponent<Transform>().localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
    }

    // Activates Capture Field
    // not currently in use
    public void CaptureFieldActivator(int Activate)
    {
        switch (Activate)
        {
            case 0:
                Debug.Log("CaputureFieild");
                captureField.SetActive(true);
                break;
            case 1:
                captureField.SetActive(false);
                break;
        }
    }

    // Destroys this game object in 0.1 sec
    IEnumerator DestroyFruitWithDelay()
    {
        yield return new WaitForSeconds(0.1f);
        PhotonNetwork.Destroy(this.gameObject);
    }
}
