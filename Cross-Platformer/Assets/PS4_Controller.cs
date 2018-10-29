using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS4_Controller : MonoBehaviour {

    public GameObject PS4_Player;

    [Range(1,2)]
    public int playerNo;
    

    // Update is called once per frame
    void Update () {

        float horizontal = Input.GetAxis("Horizontal_" + playerNo.ToString());
        if (horizontal > 0.1f)
        {
            PS4_Player.SendMessage("OnHorizontal_pos");
        }
        if (horizontal < -0.1f)
        {
            PS4_Player.SendMessage("OnHorizontal_neg");
        }
        if (horizontal > -0.1f && horizontal < 0.1f)
        {
            PS4_Player.SendMessage("OnHorizontal_reset");
        }

        float vertical = Input.GetAxis("Vertical_" + playerNo.ToString());
        if (vertical > 0.1f)
        {
            PS4_Player.SendMessage("OnVertical_pos");
        }
        if (vertical < -0.1f)
        {
            PS4_Player.SendMessage("OnVertical_neg");
        }

        if (Input.GetButtonDown("Fire1"))
        {
            PS4_Player.SendMessage("OnFire1");
        }

        if (Input.GetButtonDown("Jump"))
        {
            PS4_Player.SendMessage("OnJump");
        }

	}
}
