using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Controller : MonoBehaviour {

    public GameObject PC_Player1;

    // Update is called once per frame
    void Update () {

		if (Input.GetButtonDown("Fire1"))
        {
            PC_Player1.SendMessage("OnFire1");
        }

	}
}
