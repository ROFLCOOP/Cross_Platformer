using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDebugTest : MonoBehaviour {

    //GameObject Controller;

    [Range(1,2)]
    public int PlayerNo;

    Vector3 origin;
    public float yVel;
  
    // Use this for initialization
    void Start () {
        //Controller = GameObject.Find("PS4_Inbox");
        origin = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnHorizontal_pos()
    {
        transform.Translate(5, 0, 0);
        Debug.Log("HOZ_POS");
    }

    void OnHorizontal_neg()
    {
        transform.Translate(-5, 0, 0);
        Debug.Log("HOZ_NEG");
    }

    void OnHorizontal_reset()
    {
        transform.Translate(0,yVel,0);
        Debug.Log("HOZ_RES");
    }

    void OnJump()
    {
        Debug.Log("JUMP");
    }

   //void OnFire1()
   //{
   //    if (colourSelected) { colourSelected = false; }
   //    Debug.Log("Back Button Pressed");
   //}
   //
   //void ActivatePlayer()
   //{
   //    hasController = true;
   //    myTexture = activated_mat;
   //    renderer.material = activated_mat;
   //}
   //
   //void OnStart()
   //{
   //    if (playerNo == 1) { Controls_Manager.SendMessage("LoadScene"); }
   //}
}
