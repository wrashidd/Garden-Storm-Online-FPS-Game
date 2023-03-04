using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for spawning defender team players
Work in progress
*/
public class SpawnpointDefenders : MonoBehaviour
{
    [SerializeField]
    private GameObject playerSpawnGraphics;

    // Runs before Start
    private void Awake()
    {
        playerSpawnGraphics.SetActive(false);
    }
}
