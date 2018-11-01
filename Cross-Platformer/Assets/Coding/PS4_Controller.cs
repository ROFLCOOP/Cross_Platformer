using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS4_Controller : MonoBehaviour {

    Controller_Management_Behaviour controller;

    void Start()
    {
        controller = gameObject.GetComponent<Controller_Management_Behaviour>();
        GameObject.DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update () {

        for(int i = 0;i < 4;++i)
        {
            if (!controller.Joystick_Player_Map.ContainsKey(i)) continue;

            Controller_Management_Behaviour.PlayerInfo info = controller.Joystick_Player_Map[i];


            GameObject PS4_Player = controller.Joystick_Player_Map[i].playerObject;

            

            float horizontal = Input.GetAxis("Horizontal_" + i.ToString()); 
            //Horizontal pos = LS pushed right, neg = LS pushed Left, reset = LS returned to horizontal centre
            if (horizontal > 0.2f)  
            {
                PS4_Player.SendMessage("OnHorizontal_pos");
            }
            if (horizontal < -0.2f)
            {
                PS4_Player.SendMessage("OnHorizontal_neg"); 
            }
            if (horizontal > -0.2f && horizontal < 0.2f)
            {
                PS4_Player.SendMessage("OnHorizontal_reset");
            }

            float vertical = Input.GetAxis("Vertical_" + i.ToString());
            //Vertical pos = LS pushed up, neg = LS pushed down,
            if (vertical < -0.1f) //Vertical scale has been inverted
            {
                PS4_Player.SendMessage("OnVertical_neg");
            }
            if (vertical > 0.1f)
            {
                PS4_Player.SendMessage("OnVertical_pos");
            }
    

            if (Input.GetButtonDown("Fire1_" + i.ToString())) //Fire1 button is Circle
            {
                PS4_Player.SendMessage("OnFire1");
            }



            if (Input.GetButtonDown("Jump_" + i.ToString())) //Jump button is Cross
            {
                PS4_Player.SendMessage("OnJump");
            }



            if (Input.GetButtonDown("Options_" + i.ToString())) 
            {
                PS4_Player.SendMessage("OnStart");
            }

        }

            

	}


}
