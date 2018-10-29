using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_Judge_Script : MonoBehaviour {

    void OnHorizontal_pos()
    {
        Debug.Log("Horizontal Axis Positive button Pressed");
    }

    void OnHorizontal_neg()
    {
        Debug.Log("Horizontal Axis Negative button Pressed");
    }

    void OnVertical_neg()
    {
        Debug.Log("Vertical Axis Negative button Pressed");
    }

    void OnVertical_pos()
    {
        Debug.Log("Vertical Axis Positive button Pressed");
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
