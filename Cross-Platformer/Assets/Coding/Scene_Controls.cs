using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Controls : MonoBehaviour {

    bool P1Startup = false;
    bool P2Startup = false;

    bool P1Ready = false;
    bool P2Ready = false;

    public GameObject P1Sphere;
    public GameObject P2Sphere;

    void PlayerXisReady (int playerNo)
    {
        if (playerNo == 1) { P1Ready = true; }
        if (playerNo == 2) { P2Ready = true; }
    }

    void PlayerXisNotReady(int playerNo)
    {
        if (playerNo == 1) { P1Ready = false; }
        if (playerNo == 2) { P2Ready = false; }
    }

    void OnStartupPlayer()
    {
        if (!P1Startup)
        {
            P1Startup = true;
            P1Sphere.SendMessage("StartUp");
        }
    }
}
