using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller_Management_Behaviour : MonoBehaviour {

    public GameObject PlayerObject_1;
    public GameObject PlayerObject_2;

    Color[] palette = new Color[6] { Color.red, Color.magenta, Color.blue, Color.cyan, Color.green, Color.yellow };


    bool m_findingPlayerControllers = true;

    bool P1Ready = false;
    bool P2Ready = false;

    public struct PlayerInfo
    {
        public PlayerInfo(Controller_Management_Behaviour manager, int i)
        {
            PlayerNo = i;
            Colour = Color.red;
            playerObject = null;
            if (PlayerNo == 0)         { playerObject = manager.PlayerObject_1; }
            else if (PlayerNo == 1)    { playerObject = manager.PlayerObject_2; }
            else                { Debug.Log("Error Player has no assigned object"); }
        }
        public int PlayerNo;
        public Color Colour;
        public GameObject playerObject;
    }

    public Dictionary<int, PlayerInfo> Joystick_Player_Map;

    

    int currentNumberPlayers = 0;

    public string NextScene;


	// Use this for initialization
	void Start () {
        Joystick_Player_Map = new Dictionary<int, PlayerInfo>();
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
                if (!Joystick_Player_Map.ContainsKey(joystickNo))
                {
                    Joystick_Player_Map[joystickNo] = new PlayerInfo(this, currentNumberPlayers);
                    Joystick_Player_Map[joystickNo].playerObject.SendMessage("ActivatePlayer");
                    currentNumberPlayers++;
                }

                
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

    void AssignPlayerColour(int codeNo)
    {
        int joystick = codeNo / 10;
        int colourCode = codeNo % 10;

        Color hue = palette[colourCode];

        PlayerInfo Data = Joystick_Player_Map[joystick];
        Data.Colour = hue;
        Joystick_Player_Map[joystick] = Data;
    }

    void ReadyPlayer (int player)
    {
        if (player == 1)        { P1Ready = true; }
        else if (player == 2)   { P2Ready = true; }
    }

    void LoadScene()
    {
        if (P1Ready && P2Ready)
        {
            SceneManager.LoadScene(NextScene);
        }
    }
}
