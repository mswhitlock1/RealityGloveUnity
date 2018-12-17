using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class ReadOrientation : MonoBehaviour {
    private float lastNumber;
    private string lastColor;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        string inUse = GameObject.Find("ReadFinger").GetComponent<ReadFinger>().getInUse();
        if (inUse == "pinky")
        {
            GameObject.Find("Knob").GetComponent<KnobControl>().knobDelta(-lastNumber);
        }
        else if (inUse == "middle")
        {
            GameObject.Find("ColorOptions").GetComponent<ColorOptions>().updateColor(lastColor);
        }
        else if (inUse == "ring")
        {
            GameObject.Find("ScaleCube").GetComponent<ScaleIronman>().updateScale(-lastNumber);
        }
        else if (inUse == "index")
        {
            GameObject.Find("ShadeSphere").GetComponent<UpdateSphereColor>().updateColor(-lastNumber);
        }
    }
    void OnMessageArrived(string msg)
    {
        Debug.Log(msg);
        if (!float.TryParse(msg, NumberStyles.Any, CultureInfo.InvariantCulture, out lastNumber))
        {
            lastColor = msg;
        }
    }
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Orientation Connection established");
        else
            Debug.Log("Orientation Connection attempt failed or disconnection detected");
    }
}
