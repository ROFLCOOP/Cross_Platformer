using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS4_Controller : MonoBehaviour {

    public GameObject PS4_Player;

    [Range(1,2)]
    public int playerNo;

    public string LS_Horizontal_positive;
    public string LS_Horizontal_negative;
    public string LS_Horizontal_reset;

    public string LS_Vertical_positive;
    public string LS_Vertical_negative;

    public string Button_Fire1;
    public string Button_Jump;

    // Update is called once per frame
    void Update () {

        float horizontal = Input.GetAxis("Horizontal_" + playerNo.ToString());
        if (horizontal > 0.1f)
        {
            if (LS_Horizontal_positive != null) { PS4_Player.SendMessage(LS_Horizontal_positive); }
            else                                { Debug.Log("Left Stick pushed right, but there is no function listed"); }
        }
        if (horizontal < -0.1f)
        {
            if (LS_Horizontal_negative != null) { PS4_Player.SendMessage(LS_Horizontal_negative); }
            else                                { Debug.Log("Left Stick pushed left, but there is no function listed"); }
        }
        if (horizontal > -0.1f && horizontal < 0.1f)
        {
            if (LS_Horizontal_reset != null)    { PS4_Player.SendMessage(LS_Horizontal_reset); }
            else                                { Debug.Log("Left Stick returned to horizontal center, no function listed"); }
        }

        float vertical = Input.GetAxis("Vertical_" + playerNo.ToString());
        if (vertical < -0.1f) //Vertical scale has been inverted
        {
            if (LS_Vertical_positive != null)   { PS4_Player.SendMessage(LS_Vertical_positive); }
            else                                { Debug.Log("Left Stick pushed up, but there is no function listed"); }
        }
        if (vertical > 0.1f) 
        {
            if (LS_Vertical_negative != null)   { PS4_Player.SendMessage(LS_Vertical_negative); }
            else                                { Debug.Log("Left Stick pushed down, but there is no function listed"); }
        }
    

        if (Input.GetButtonDown("Fire1_" + playerNo.ToString()))
        {
            if (Button_Fire1 != null)           { PS4_Player.SendMessage(Button_Fire1); }
            else                                { Debug.Log("Fire1 Button Pressed, but there is no function listed"); }
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (Button_Jump != null)            { PS4_Player.SendMessage(Button_Jump); }
            else                                { Debug.Log("Jump Button Pressed, but there is no function listed"); }
        }

	}
}
