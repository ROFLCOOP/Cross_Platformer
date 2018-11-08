using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS4_Controller : MonoBehaviour {

    Controller_Management_Behaviour controller;

    GameObject PlayerObject_1;
    GameObject PlayerObject_2;

    public class InputPacket //Holds data for Left & Right Stick Movement, and Jump Button
    {
        public InputPacket(float LS_H, float LS_V, float RS_H, float RS_V, bool BT_J)
        {
            LS_Horiz    = LS_H;
            LS_Vert     = LS_V;
            RS_Horiz    = RS_H;
            RS_Vert     = RS_V;
            btn_jump    = BT_J;
        }
        public float LS_Horiz;
        public float LS_Vert;
        public float RS_Horiz;
        public float RS_Vert;
        public bool btn_jump;
    }

    void Start()
    {
        controller = gameObject.GetComponent<Controller_Management_Behaviour>();
        GameObject.DontDestroyOnLoad(gameObject);
        OnSceneLoad();
    }

    // Update is called once per frame
    void Update () {

        for(int i = 0;i < 5;++i)
        {
            if (!(controller.Joystick_Player_Map.ContainsKey(i))) continue;

            Controller_Management_Behaviour.PlayerInfo info = controller.Joystick_Player_Map[i];

            if (info.playerObject == null) { OnSceneLoad();  }
            GameObject PS4_Player = info.playerObject;
            


            float LShorizontal = Input.GetAxis("LS_Horizontal_" + i.ToString());
            //LSHorizontal pos = LS pushed right, neg = LS pushed Left

            float LSvertical = Input.GetAxis("LS_Vertical_" + i.ToString());
            //LSVertical pos = LS pushed up, neg = LS pushed down,


            float RShorizontal = Input.GetAxis("RS_Horizontal_" + i.ToString());
            //RSHorizontal pos = RS pushed right, neg = RS pushed Left
            
            float RSvertical = Input.GetAxis("RS_Vertical_" + i.ToString());
            //RSVertical pos = RS pushed up, neg = RS pushed down,

            //bool BTNfire1;
            //if (Input.GetButtonDown("Fire1_" + i.ToString())) //Fire1 button is Circle
            //{
            //    BTNfire1 = true;
            //}
            //else
            //{
            //    BTNfire1 = false;
            //}


            bool BTNjump;
            if (Input.GetButtonDown("Jump_" + i.ToString())) //Jump button is Cross
            {
                BTNjump = true;
            }
            else
            {
                BTNjump = false;
            }
            InputPacket ctrl_arr = new InputPacket(LShorizontal, LSvertical, RShorizontal, RSvertical, BTNjump);
            PS4_Player.SendMessage("processInput", ctrl_arr);


            if (Input.GetButtonDown("Options_" + i.ToString())) 
            {
                PS4_Player.SendMessage("OnStart");
            }


        }

            

	}

    public void OnSceneLoad()
    {
        PlayerObject_1 = GameObject.FindGameObjectWithTag("P1Object");
        PlayerObject_2 = GameObject.FindGameObjectWithTag("P2Object");
        for (int i = 0; i < 5; ++i)
        {
            if (!(controller.Joystick_Player_Map.ContainsKey(i)))
            {
                continue;
            }

            Controller_Management_Behaviour.PlayerInfo info = controller.Joystick_Player_Map[i];

            if (info.PlayerNo == 0)
            {
                info.playerObject = PlayerObject_1;
            }

            if (info.PlayerNo == 1)
            {
                info.playerObject = PlayerObject_2;
            }

        }
    }


}
