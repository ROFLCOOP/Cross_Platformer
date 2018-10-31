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

    bool hasController = false;

    public GameObject Controls_Manager;

    public Text label;

    //Controller ID


    [Range(1,2)]
    public int playerNo = 1;

    public Material activated_mat;
    GameObject Scene_Controls;

    bool colourSelected = false;
    public class assignControllers
    {
        public assignControllers(int i) { Player = i; }
        int Player;
        public bool HasControllerAssigned;
    };
    List<assignControllers> playerSpaces;

    MeshRenderer renderer;
    private object controller;

    // Use this for initialization
    void Start () {
        myTexture = GetComponent<MeshRenderer>().material;
        Scene_Controls = GameObject.Find("Scene_Director");
        Controls_Manager = GameObject.Find("PS4_Inbox");

        renderer = GetComponent<MeshRenderer>();
    }

    void StartUp()
    {
        OnSelect();
        hasController = true;
        //Set Controller ID
    }
	
	// Update is called once per frame
    void OnHorizontal_pos()
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

    void OnHorizontal_neg()
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

    void OnHorizontal_reset()
    {
        horiz_pos_press = false;
        horiz_neg_press = false;
    }

    void OnSelect()
    {
        if (hasController)
        {
            int codeNumber = playerNo * 10 + colourNo;
            if (!colourSelected) { Controls_Manager.SendMessage("AssignColour", codeNumber); label.color = palette[colourNo]; Controls_Manager.SendMessage("ReadyPlayer", playerNo); }
            Debug.Log("Select Button Pressed");
        }
        
    }

    void OnBack()
    {
        if (colourSelected) { colourSelected = false; }
        Debug.Log("Back Button Pressed");
    }

    void ActivatePlayer()
    {
        hasController = true;
        myTexture = activated_mat;
        renderer.material = activated_mat;
    }

    void OnStart()
    {
        if (playerNo == 1) { Controls_Manager.SendMessage("LoadScene"); }
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
