using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for spawning intruder team players
Work in progress
*/
public class SpawnpointIntruders : MonoBehaviour
{
    [SerializeField]
    private GameObject playerSpawnGraphics;

    private void Awake()
    {
        playerSpawnGraphics.SetActive(false);
    }
}
