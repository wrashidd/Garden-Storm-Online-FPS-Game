﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;
//using Photon.Pun.Demo.PunBasics;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{

    public static RoomManager Instance;

    private void Awake()
    {
        if (Instance) // it checks if another RoomManager exists.
        {
            Destroy(gameObject); // because only one can exist.
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

  public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

  public override void OnDisable()
  {
      base.OnDisable();
      SceneManager.sceneLoaded -= OnSceneLoaded;
  }

  void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
  {
      if (scene.buildIndex == 1) // First Garden 
      {
          PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
      }
  }
  // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
