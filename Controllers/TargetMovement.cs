using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This class is responsible for bots targeted navigation
Not currently in use
*/
public class TargetMovement : MonoBehaviour
{
    public bool bShouldMove,
        bShouldRotate;

    public float moveSpeed,
        rotateSpeed;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (bShouldMove)
        {
            transform.position += new Vector3(moveSpeed, 0f, 0f) * Time.deltaTime;
        }

        if (bShouldRotate)
        {
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles + new Vector3(0f, rotateSpeed * Time.deltaTime, 0f)
            );
        }
    }
}
