using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UmpireScript : MonoBehaviour {

    [Range(0,10)]
    public int RoundLength_mins;

    public int RoundLength_secs;

    float Tot_RemainingSecs;

    public Text TXT_timer;
    public Canvas Gameplay;
    public Canvas GameOver;

    int P1Score;
    int P2Score;
    Text P1ScoreCounter;
    Text P2ScoreCounter;
    Text P1ScoreTitle;
    Text P2ScoreTitle;


    int P1ColourCode;
    int P2ColourCode;

    Text TXT_OverLeftPanel;
    Text TXT_OverRightPanel;

    Color[] palette = new Color[6] { Color.red, Color.magenta, Color.blue, Color.cyan, Color.green, Color.yellow }; 
    Color[] lt_palette = new Color[6] { new Color(1.0f, 0.58f, 0.58f, 0.6f), new Color(1.0f, 0.58f, 1.0f, 0.6f), new Color(0.58f, 0.58f, 1.0f, 0.6f), new Color(0.58f, 1.0f, 1.0f, 0.6f), new Color(0.58f, 1.0f, 0.58f, 0.6f), new Color(1.0f, 1.0f, 0.58f, 0.6f) };
    Color[] dk_palette = new Color[6] { new Color(0.39f, 0, 0, 0.6f), new Color(0.39f, 0, 100, 0.6f), new Color(0, 0, 0.39f, 0.6f), new Color(0, 0.39f, 0.39f, 0.6f), new Color(0, 0.39f, 0, 0.6f), new Color(0.39f, 0.39f, 0, 0.6f) };

    Controller_Management_Behaviour controller;


    // Use this for initialization
    void Start () {
        Tot_RemainingSecs = (RoundLength_mins * 60) + RoundLength_secs;
        
        Gameplay.enabled = true;
        GameOver.enabled = false;

        TXT_OverLeftPanel = GameObject.Find("Left_Panel_Text").GetComponent<Text>();
        TXT_OverRightPanel = GameObject.Find("Right_Panel_Text").GetComponent<Text>();

        P1ScoreCounter = GameObject.Find("P1_ScoreNo").GetComponent<Text>();
        P2ScoreCounter = GameObject.Find("P2_ScoreNo").GetComponent<Text>();

        P1ScoreTitle = GameObject.Find("P1_ScoreTitle").GetComponent<Text>();
        P2ScoreTitle = GameObject.Find("P2_ScoreTitle").GetComponent<Text>();

        controller = GameObject.Find("PS4_Inbox").GetComponent<Controller_Management_Behaviour>();
        for (int i = 0; i < 5; ++i)
        {
            if (!controller.Joystick_Player_Map.ContainsKey(i)) continue;

            if (controller.Joystick_Player_Map[i].PlayerNo == 0)
            {
                P1ColourCode = controller.Joystick_Player_Map[i].ColourCode;
            }
            else if (controller.Joystick_Player_Map[i].PlayerNo == 1)
            {
                P2ColourCode = controller.Joystick_Player_Map[i].ColourCode;
            }

        }

        P1ScoreTitle.color = palette[P1ColourCode];
        P1ScoreCounter.color = palette[P1ColourCode];
        P2ScoreTitle.color = palette[P2ColourCode];
        P2ScoreCounter.color = palette[P2ColourCode];
        P1Score = 0;
        P2Score = 0;
    }
	
	// Update is called once per frame
	void Update () {
        Tot_RemainingSecs -= Time.deltaTime;

        int Dis_RemainingMins = Mathf.FloorToInt(Tot_RemainingSecs) / 60;
        int Dis_RemainingSecs = Mathf.FloorToInt(Tot_RemainingSecs) % 60;

        if (Dis_RemainingSecs > 9) { TXT_timer.text = Dis_RemainingMins.ToString() + ":" + Dis_RemainingSecs.ToString(); }
        else                       { TXT_timer.text = Dis_RemainingMins.ToString() + ":0" + Dis_RemainingSecs.ToString(); }

        if (Tot_RemainingSecs <= 0) { runGameOver(); }
    }

    public void scorePoint (int player)
    {
        if (player == 1)            { P1Score++;}
        else if (player == 2)       { P2Score++; }
        else                        { Debug.Log("Error:Invalid Player Number called on scorePoint function"); }

        if (P1Score < 10)           { P1ScoreCounter.text = "0" + P1Score.ToString(); }
        else                        { P1ScoreCounter.text = P1Score.ToString(); }

        if (P2Score < 10)           { P2ScoreCounter.text = "0" + P2Score.ToString(); }
        else                        { P2ScoreCounter.text = P2Score.ToString(); }
    }

    void runGameOver()
    {
        Image LPan = GameObject.Find("Left_Panel").GetComponent<Image>();
        Image RPan = GameObject.Find("Right_Panel").GetComponent<Image>();
        if (P1Score > P2Score)
        {
            LPan.color = lt_palette[P1ColourCode];
            RPan.color = dk_palette[P2ColourCode];
            TXT_OverRightPanel.text = "Game\nover";
            TXT_OverRightPanel.color = Color.black;
        }
        else if (P1Score < P2Score)
        {
            LPan.color = dk_palette[P1ColourCode];
            RPan.color = lt_palette[P2ColourCode];
            TXT_OverLeftPanel.text = "Game\nover";
            TXT_OverLeftPanel.color = Color.black;
        }
        else 
        {
            LPan.color = dk_palette[P1ColourCode];
            RPan.color = dk_palette[P2ColourCode];
            TXT_OverLeftPanel.text = "Game\nover";
            TXT_OverLeftPanel.color = Color.black;
            TXT_OverRightPanel.text = "Game\nover";
            TXT_OverRightPanel.color = Color.black;
        }
        Gameplay.enabled = false;
        GameOver.enabled = true;
    }
}
