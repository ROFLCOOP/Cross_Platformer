using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_Judge_Script : MonoBehaviour {

    void OnHorizontal_pos()
    {
        Debug.Log("Left stick pushed to right");
    }

    void OnHorizontal_neg()
    {
        Debug.Log("Left stick pushed to left");
    }

    void OnVertical_neg()
    {
        Debug.Log("Left stick pushed downwards");
    }

    void OnVertical_pos()
    {
        Debug.Log("Left stick pushed upwards");
    }

    void OnJump()
    {
        Debug.Log("Jump butoon Pressed");   
    }

    void OnFire1()
    {
        Debug.Log("Fire1 button Pressed");
    }
}
