using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for managing aiding activity in game
Work in progress
*/
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    // Locks mouse cursor
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
}
