using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Object = System.Object;

/*
This class is responsible for animating Player's Jets on jump
*/
public class PlayerJets : MonoBehaviour
{
    private PhotonView PV;

    // Runs before Start
    // Accesses Photon networking system
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    // Tracks user inputs
    void Update()
    {
        if (!PV.IsMine)
            return;

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

        if (
            Input.GetKeyDown(KeyCode.Space)
            || Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.A)
            || Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.D)
            || Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.W)
        )
        {
            StartCoroutine(DoubleJetRoutine());
        }
    }

    // Fixed Updated is called every fixed frame-rate frame
    // Checks of current Photon view is local
    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;
    }

    IEnumerator DoubleJetRoutine()
    {
        transform
            .GetChild(0)
            .GetComponent<Transform>()
            .GetComponent<ParticleSystem>()
            .startLifetime = 0.6f;
        Debug.Log("Particles boosted");
        yield return new WaitForSeconds(0.25f);
        transform
            .GetChild(0)
            .GetComponent<Transform>()
            .GetComponent<ParticleSystem>()
            .startLifetime = 0.1f;
    }
}
