using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS4_Controller : MonoBehaviour {

    public GameObject PS4_Player;

	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            PS4_Player.SendMessage("OnJump");
        }

	}
}
