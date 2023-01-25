using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointIntruders : MonoBehaviour
{
    [SerializeField] private GameObject playerSpawnGraphics;


    private void Awake()
    {
        playerSpawnGraphics.SetActive(false);
    }
}
