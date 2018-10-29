using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colour_Select : MonoBehaviour {

    Material myTexture;

    Color[] palette = new Color[6]{ Color.red, Color.magenta, Color.blue, Color.cyan, Color.green, Color.yellow };
    int colourNo = 0;

    bool horiz_pos_press = false;

	// Use this for initialization
	void Start () {
        myTexture = GetComponent<MeshRenderer>().material;
	}
	
	// Update is called once per frame
    void OnHorizontal_pos()
    {
        if (!horiz_pos_press)
        {
            if (colourNo < 6) { colourNo++; }
            else { colourNo = 0; }
            myTexture.color = palette[colourNo];
            Debug.Log("colour change!");
            horiz_pos_press = true;
        }
    }

    void OnHorizontal_reset()
    {
        horiz_pos_press = false;
    }
}
