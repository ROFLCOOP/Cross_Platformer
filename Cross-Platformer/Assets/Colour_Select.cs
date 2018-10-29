using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colour_Select : MonoBehaviour {

    Material myTexture;

    Color[] palette = new Color[6]{ Color.red, Color.magenta, Color.blue, Color.cyan, Color.green, Color.yellow };
    int colourNo = 0;

    bool horiz_pos_press = false;
    bool horiz_neg_press = false;

    public Text PlayerText;
    bool colourSelected = false;

	// Use this for initialization
	void Start () {
        myTexture = GetComponent<MeshRenderer>().material;
	}
	
	// Update is called once per frame
    void OnHorizontal_pos()
    {
        horiz_neg_press = false;
        if (!horiz_pos_press)
        {
            if (colourNo < 6) { colourNo++; }
            else { colourNo = 0; }
            myTexture.color = palette[colourNo];
            Debug.Log("colour change!");
            
        }
        horiz_pos_press = true;
    }

    void OnHorizontal_neg()
    {
        horiz_pos_press = false;
        if (!horiz_pos_press)
        {
            if (colourNo < 5) { colourNo++; }
            else { colourNo = 0; }
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
        if (!colourSelected) { PlayerText.color = myTexture.color; colourSelected = true; }
    }

    void OnBack()
    {
        if (colourSelected) { PlayerText.color = Color.white; colourSelected = false; }
    }
}
