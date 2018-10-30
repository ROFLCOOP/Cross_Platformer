using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller_Management_Behaviour : MonoBehaviour {


    bool m_findingPlayerControllers = true;

    Dictionary<int, int> Joystick_Player_Map;

    int currentNumberPlayers = 0;

    public string NextScene;

	// Use this for initialization
	void Start () {
        Joystick_Player_Map = new Dictionary<int, int>();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_findingPlayerControllers)
        {
            RunFindControllerPlayer();
        }
	}

    void RunFindControllerPlayer()
    {
        for (int joystickNo = 1; joystickNo < 5; ++joystickNo)
        {
            if (Input.GetButtonDown("Jump_" + joystickNo.ToString()))
            {
                Debug.Log("Jump Button Pressed on" + joystickNo.ToString());
                Joystick_Player_Map[currentNumberPlayers] = joystickNo;
                currentNumberPlayers++;
            }
        }

        if (currentNumberPlayers > 0)
        {
            if (Input.GetButtonDown("Options_" + Joystick_Player_Map[0].ToString()))
            {
                SceneManager.LoadScene(NextScene);
                Debug.Log("Options Button Pressed on Player 1's controller");
            }
        }
    }
}
