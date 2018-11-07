﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Colour_Select : MonoBehaviour {

    Material myTexture;

    Color[] palette = new Color[6] { Color.red, Color.magenta, Color.blue, Color.cyan, Color.green, Color.yellow };

    int colourNo = 0;
    bool horiz_pos_press = false;
    bool horiz_neg_press = false;



    public GameObject Controls_Manager;

    public Text label;

    //Controller ID


    [Range(1,2)]
    public int playerNo = 1;

    public Material activated_mat;
    //GameObject Scene_Controls;

    bool colourSelected = false;

    new MeshRenderer renderer;
    private object controller;
    Controller_Management_Behaviour control_mnge_funcs;
    PS4_Controller inputs_funcs;

    // Use this for initialization
    void Start () {
        myTexture = GetComponent<MeshRenderer>().material;

        //Scene_Controls = GameObject.Find("Scene_Director");
        Controls_Manager = GameObject.Find("PS4_Inbox");
        control_mnge_funcs = Controls_Manager.GetComponent<Controller_Management_Behaviour>();
        inputs_funcs = Controls_Manager.GetComponent<PS4_Controller>();

        renderer = GetComponent<MeshRenderer>();
    }

    void StartUp()
    {
        OnJump();
    }
	void processInput(PS4_Controller.InputPacket inputPacket)
    {
        float LS_Horizontal = inputPacket.LS_Horiz;
        if (LS_Horizontal > 0.1f)           { OnHorizontal_pos(); }
        else if (LS_Horizontal < -0.1f)     { OnHorizontal_neg(); }
        else                                { OnHorizontal_reset(); }

        if (inputPacket.btn_jump)           { OnJump(); } 
    }
	// Update is called once per frame
    public void OnHorizontal_pos()                  //When Controller is pushed to the right it cycles through the colour choices
    {
        horiz_neg_press = false;
        if (!horiz_pos_press)
        {
            if (colourNo < 5) { colourNo++; }
            else { colourNo = 0; }
            myTexture.color = palette[colourNo];
            Debug.Log("colour change!");
            horiz_pos_press = true;
        }
    }

    public void OnHorizontal_neg()                  //When Controller is pushed to the left it cycles through the colour choices
    {
        horiz_pos_press = false;
        if (!horiz_neg_press)
        {
            if (colourNo > 0) { colourNo--; }
            else { colourNo = 5; }
            myTexture.color = palette[colourNo];
            Debug.Log("colour change!");
            horiz_neg_press = true;
        }
    }

    public void OnHorizontal_reset()                
    {
        horiz_pos_press = false;
        horiz_neg_press = false;
    }

    public void OnJump()                            //Jump button is used as a select item, will only be trigger after assigned to a controller
    {
        if (!colourSelected)
        {
            control_mnge_funcs.AssignPlayerColour(playerNo-1, colourNo);
            label.color = palette[colourNo];
            control_mnge_funcs.ReadyPlayer(playerNo);
        }
        Debug.Log("Select Button Pressed");
    }

    public void ActivatePlayer()                    //Activates Player for when player has reached the "assigned controller" state
    {
        myTexture = activated_mat;
        renderer.material = activated_mat;
        myTexture.color = Color.red;
    }

    public void OnStart()                           //Options buttons press, Load scene will only occur if both players are ready
    {
        if (playerNo == 0) { Controls_Manager.GetComponent<Controller_Management_Behaviour>().LoadScene(); }
    }

}
