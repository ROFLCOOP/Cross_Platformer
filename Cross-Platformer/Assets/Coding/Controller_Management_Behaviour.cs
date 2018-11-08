using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller_Management_Behaviour : MonoBehaviour
{

    GameObject PlayerObject_1;
    GameObject PlayerObject_2;

    Color[] palette = new Color[6] { Color.red, Color.magenta, Color.blue, Color.cyan, Color.green, Color.yellow };


    bool m_findingPlayerControllers = true;

    bool P1Ready = false;
    bool P2Ready = false;

    private int controllingJoystick = 0;

    public class PlayerInfo
    {
        public PlayerInfo(Controller_Management_Behaviour manager, int i)
        {
            PlayerNo = i;
            ColourCode = 0;
            playerObject = null;
            if (PlayerNo == 0) { playerObject = manager.PlayerObject_1; }
            else if (PlayerNo == 1) { playerObject = manager.PlayerObject_2; }
            else { Debug.Log("Error Player has no assigned object"); }
        }
        public int PlayerNo;
        public int ColourCode;
        public GameObject playerObject;
    }

    public Dictionary<int, PlayerInfo> Joystick_Player_Map;



    int currentNumberPlayers = 0;

    string[] sceneList = new string[2] { "Main_Scene", "HorrorLevel" };

    
    PS4_Controller inputs_manager;

    void Start()
    {
        OnSceneLoad();
        inputs_manager = (GameObject.Find("PS4_Inbox")).GetComponent<PS4_Controller>();
    }
    // Use this for initialization
    void Awake()
    {
        Joystick_Player_Map = new Dictionary<int, PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
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
                if (!Joystick_Player_Map.ContainsKey(joystickNo))
                {
                    Joystick_Player_Map[joystickNo] = new PlayerInfo(this, currentNumberPlayers);
                    Joystick_Player_Map[joystickNo].playerObject.GetComponent<Colour_Select>().ActivatePlayer();
                    currentNumberPlayers++;
                    if(currentNumberPlayers == 1)
                    {
                        controllingJoystick = joystickNo;
                    }
                }


            }
        }

        if (currentNumberPlayers > 0)
        {
            if (Input.GetButtonDown("Options_" + controllingJoystick))
            {
                LoadScene();
            }
        }
    }

    public void AssignPlayerColour(int playerNo, int colourCode)
    {
        int joystick = 0;
        for (int i = 0; i < 5; ++i)
        {
            if (!Joystick_Player_Map.ContainsKey(i)) continue;

            if (Joystick_Player_Map[i].PlayerNo == playerNo)
            {
                joystick = i;
            }
        }

        PlayerInfo Data = Joystick_Player_Map[joystick];
        Data.ColourCode = colourCode;
        Joystick_Player_Map[joystick] = Data;
    }

    public void ReadyPlayer(int player)
    {
        if (player == 1) { P1Ready = true; }
        else if (player == 2) { P2Ready = true; }
    }

    public void LoadScene()
    {
        if (P1Ready && P2Ready)
        {
            int sceneNo = Random.Range(0, 2);
            string NextScene = sceneList[sceneNo];
            SceneManager.LoadScene(NextScene);
            OnSceneLoad();
            inputs_manager.OnSceneLoad();
        }
    }
    public void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        OnSceneLoad();
        inputs_manager.OnSceneLoad();
    }

    public void OnSceneLoad()
    {
        PlayerObject_1 = GameObject.FindGameObjectWithTag("P1Object");
        PlayerObject_2 = GameObject.FindGameObjectWithTag("P2Object");
        for (int i = 0; i < 5; ++i)
        {
            if (!Joystick_Player_Map.ContainsKey(i)) continue;

            PlayerInfo info = Joystick_Player_Map[i];

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
