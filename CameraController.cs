using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // LateUpdate is called once per frame if the behavior is enabled
    void LateUpdate()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}
