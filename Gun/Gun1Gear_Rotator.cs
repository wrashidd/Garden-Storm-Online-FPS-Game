using System;
using UnityEngine;

/*
This class is responsible for main gun's gear part rotation
*/
public class Gun1Gear_Rotator : MonoBehaviour
{
    public static Gun1Gear_Rotator Instance;
    public Animator anim;

    // Update is called once per frame
    // Runs Single and Multiple rotation sequences
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
