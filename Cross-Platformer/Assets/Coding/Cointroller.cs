using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Cointroller : MonoBehaviour {

	const int HorrorCoinAmount	= 7;
	const int SkyCoinAmount	= 9;

    Vector3[] hrr_coin_spots = new Vector3[HorrorCoinAmount] { new Vector3(-9.87f, 15.33f, 2.35f), new Vector3(8.54f, 15.33f, 5.61f), new Vector3(-0.61f, 15.33f, 7.78f), new Vector3(22.1f, 7.76f, -1.73f), new Vector3(-3.55f, 7.76f, 10.62f), new Vector3(12.78f, 7.76f, 29.94f), new Vector3(-33.47f, 7.0f, 22.71f) };
    // Vector3[] sky_coin_spots = new Vector3[SkyCoinAmount];


    string currSceneName;

    GameObject P1Obj;
    GameObject P2Obj;

    UmpireScript umpire_fnc;


    // Use this for initialization
    void Start () {
        currSceneName = (SceneManager.GetActiveScene()).name;
        umpire_fnc = GameObject.Find("Umpire").GetComponent<UmpireScript>();
        if (currSceneName == "HorrorLevel" || currSceneName == "Main_Scene")  {
            //moveCoin();
            transform.position =  new Vector3(9.5f, 3.1f, -5.4f);
        }
        else                                                                  { Destroy(this); }
        P1Obj = GameObject.Find("P1Capsule");
        P2Obj = GameObject.Find("P2Capsule");
    }
	
	// Update is called once per frame
	void Update () {
        Quaternion turn = new Quaternion(-0, 0f, -0.05f, 1);
        transform.rotation *= turn;
	}

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Trigger Enter");
        GameObject otherCollider = collision.gameObject;
        if (otherCollider == P1Obj) { umpire_fnc.scorePoint(1); }
        else if (otherCollider == P2Obj) { umpire_fnc.scorePoint(2); }
        moveCoin();
    }

    void moveCoin()
    {
        
        if (currSceneName == "HorrorLevel")
        {
			int select = Random.Range(0, 7);
			transform.position = hrr_coin_spots[select];
        }
        else if (currSceneName == "Main_Scene")
        {
			GameObject parent = GameObject.Find("CoinNodes");
			int select = Random.Range(0, SkyCoinAmount + 1);
			transform.position = parent.transform.GetChild(select).position;
			Debug.Log("Sky Level Coin Locations not set up yet!");
        }
        else
        {
            EditorUtility.DisplayDialog("ERROR", currSceneName + "does not match any scenes programmed for coin location\nSuggestion: Review moveCoin function in Cointroller Script.", "OK", "K THNX");
        }

    }
}
