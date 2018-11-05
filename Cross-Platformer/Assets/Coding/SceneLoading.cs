using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoading : MonoBehaviour {

    GameObject InputControls;

	// Use this for initialization
	void Start () {
        InputControls = GameObject.Find("PS4_Inbox");                                          //Finds the input Controls
        InputControls.GetComponent<Controller_Management_Behaviour>().OnSceneLoad();           //Runs the OnSceneLoad function (occurs after the Scene has loaded).
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
