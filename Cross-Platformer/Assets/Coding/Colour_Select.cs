using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colour_Select : MonoBehaviour {

    Material myTexture;

    public Color[] palette = new Color[6] { Color.red, Color.magenta, Color.blue, Color.cyan, Color.green, Color.yellow };

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

    // Use this for initialization
    void Start () {
        myTexture = GetComponent<MeshRenderer>().material;
        //Scene_Controls = GameObject.Find("Scene_Director");
        Controls_Manager = GameObject.Find("PS4_Inbox");

        renderer = GetComponent<MeshRenderer>();
    }

    void StartUp()
    {
        OnJump();
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
        if (!colourSelected) { label.color = palette[colourNo]; Controls_Manager.SendMessage("ReadyPlayer", playerNo); }
        Debug.Log("Select Button Pressed");
    }

    public void OnFire1()                                   //Fire1 functions as a back button, undoing the 
    {
       
    }

    public void ActivatePlayer()                    //Activates Player for when player has reached the "assigned controller" state
    {
        myTexture = activated_mat;
        renderer.material = activated_mat;
    }

    public void OnStart()                           //Options buttons press, Load scene will only occur if both players are ready
    {
        if (playerNo == 1) { Controls_Manager.SendMessage("LoadScene"); }
    }

    public void OnVertical_neg()
    {

    }
    public void OnVertical_pos()
    {

    }

    //void AddPlayerController(int Controller)
    //{
    //    playerSpaces.Add(new assignControllers(Controller));
    //    foreach (assignControllers player in playerSpaces)
    //    {
    //        if (player.HasControllerAssigned == false)
    //        {
    //            return player.AssignController(Controller);
    //        }
    //    }
    //}
}
