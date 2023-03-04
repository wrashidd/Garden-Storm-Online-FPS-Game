using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for controlling main camera logic
*/
public class CameraController : MonoBehaviour
{
    public Transform target;

    // LateUpdate is called once per frame if the behavior is enabled
    void LateUpdate()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}
