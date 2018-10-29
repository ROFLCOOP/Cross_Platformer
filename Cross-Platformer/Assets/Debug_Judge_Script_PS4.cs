using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_Judge_Script_PS4 : MonoBehaviour {

    void OnHorizontal_pos()
    {
        Debug.Log("Left Stick pushed to the right");
    }

    void OnHorizontal_neg()
    {
        Debug.Log("Left Stick pushed to the left");
    }

    void OnVertical_neg()
    {
        Debug.Log("Left Stick pushed down");
    }

    void OnVertical_pos()
    {
        Debug.Log("Left Stick pushed up");
    }

    void OnSquare()
    {
        Debug.Log("Square pressed");
    }

    void OnJump()
    {
        Debug.Log("Jump Button Pressed");
    }
}

