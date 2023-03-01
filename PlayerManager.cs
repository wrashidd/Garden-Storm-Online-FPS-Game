using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    private PhotonView PV;
    private GameObject _controller;
    public int myTeam;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine)
        {
            PV.RPC("RPC_GetTeam", RpcTarget.MasterClient);
            if (myTeam != 0)
            {
                if (myTeam == 1)
                {
                    CreateControllerDefenders();
                }
            }
            else
            {
                CreateControllerIntruders();
            }
        }
    }

    // Update is called once per frame
    void Update() { }

    void CreateControllerDefenders()
    {
        Transform _spawnpoint = SpawnManager.Instance.GetSpawnpointDefenders();
        _controller = PhotonNetwork.Instantiate(
            Path.Combine("PhotonPrefabs", "Player"),
            _spawnpoint.position,
            _spawnpoint.rotation,
            0,
            new object[] { PV.ViewID }
        ); //Instantiate Player Controller
    }

    void CreateControllerIntruders()
    {
        Transform _spawnpoint = SpawnManager.Instance.GetSpawnpointIntruders();
        _controller = PhotonNetwork.Instantiate(
            Path.Combine("PhotonPrefabs", "PlayerIntruder"),
            _spawnpoint.position,
            _spawnpoint.rotation,
            0,
            new object[] { PV.ViewID }
        ); //Instantiate Player Controller
    }

    public void GameOver()
    {
        PhotonNetwork.Destroy(_controller);
        CreateControllerDefenders();
    }

    [PunRPC]
    void RPC_GetTeam()
    {
        myTeam = SpawnManager.Instance.nextPlayerTeam;
        SpawnManager.Instance.UpdateTeam();
        PV.RPC("RPC_SentTeam", RpcTarget.OthersBuffered, myTeam);
    }

    [PunRPC]
    void RPC_SentTeam(int whichTeam)
    {
        myTeam = whichTeam;
    }
}
