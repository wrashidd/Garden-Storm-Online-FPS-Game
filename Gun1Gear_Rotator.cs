
using System;
using UnityEngine;

public class Gun1Gear_Rotator : MonoBehaviour
{
    public static Gun1Gear_Rotator Instance;
    public Animator anim;

    private void Update()
    {
        Gun1GearRotator();
        Gun1GearRotateOnce();
    }

    public void Gun1GearRotator()
    {
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("Idle", false);
        }

        if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("Idle", true);
        }
    }

    public void Gun1GearRotateOnce()
    {
        if (Input.GetMouseButtonDown(0))
        {

            anim.SetBool("Once", true);
        }
        else
        {
            anim.SetBool("Once", false);
        }
    }
}
