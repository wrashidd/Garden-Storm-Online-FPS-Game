using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Object = System.Object;

public class PlayerJets : MonoBehaviour
{
    private PhotonView PV;
    // Start is called before the first frame update


    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!PV.IsMine) return;
       
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
        {
            //transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
           // transform.GetChild(0).gameObject.SetActive(true);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
           // transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.A) || Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.D) || Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.W) )
        {
            StartCoroutine(DoubleJetRoutine());
        }
    }
    
    private void FixedUpdate()
    {
        if (!PV.IsMine) return;
    }



    IEnumerator DoubleJetRoutine()
    {
        transform.GetChild(0).GetComponent<Transform>().GetComponent<ParticleSystem>().startLifetime = 0.6f;
        Debug.Log("Particles boosted");
        yield return new WaitForSeconds(0.25f);
        transform.GetChild(0).GetComponent<Transform>().GetComponent<ParticleSystem>().startLifetime = 0.1f;

        
    }
}

