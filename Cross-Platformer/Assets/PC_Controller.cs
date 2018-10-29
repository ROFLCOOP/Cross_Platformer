using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Controller : MonoBehaviour {

    public GameObject PC_Player;

    

    // Update is called once per frame
    void Update () {

        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0.1f)
        {
            PC_Player.SendMessage("OnHorizontal_pos");
        }
        if (horizontal < -0.1f)
        {
            PC_Player.SendMessage("OnHorizontal_neg");
        }

        float vertical = Input.GetAxis("Vertical");
        if (vertical > 0.1f)
        {
            PC_Player.SendMessage("OnVertical_pos");
        }
        if (vertical < -0.1f)
        {
            PC_Player.SendMessage("OnVertical_neg");
        }

        if (Input.GetButtonDown("Fire1"))
        {
            PC_Player.SendMessage("OnFire1");
        }



        if (Input.GetButtonDown("Jump"))
        {
            PC_Player.SendMessage("OnJump");
        }

	}
}
