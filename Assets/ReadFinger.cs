using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadFinger : MonoBehaviour {
    private int[][] fingerSums;

    private int hoverThreshold = 10;
    private int pressThreshold = 10;
    private int rotatingIndex = 0;

    enum FingerType { index, middle, ring, pinky };

    private string inUse;
    // Use this for initialization
    void Start ()
    {
        inUse = "none";

        fingerSums = new int[4][];
        fingerSums[0] = new int[30];
        fingerSums[1] = new int[30];
        fingerSums[2] = new int[30];
        fingerSums[3] = new int[30];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMessageArrived(string msg)
    {
        fingerSums[0][rotatingIndex] = msg.Contains("index") ? 1 : 0;
        fingerSums[1][rotatingIndex] = msg.Contains("middle") ? 1 : 0;
        fingerSums[2][rotatingIndex] = msg.Contains("ring") ? 1 : 0;
        fingerSums[3][rotatingIndex] = msg.Contains("pinky") ? 1 : 0;

        for (int i = 0; i < 4; i++)
        {
            checkFinger(i);
        }
        rotatingIndex = (rotatingIndex + 1) % 30;
    }
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Finger Connection established");
        else
            Debug.Log("Finger Connection attempt failed or disconnection detected");
    }

    private void checkFinger(int fingerNum)
    {
        if (sum(3) > hoverThreshold)
        {
            // Rotation Manipulation
            GameObject.Find("Knob").GetComponent<KnobControl>().highlight();
            inUse = "pinky";
        }
        else if (sum(2) > hoverThreshold)
        {
            // Scale Manipulation
            GameObject.Find("ScaleCube").GetComponent<ScaleIronman>().highlight();
            inUse = "ring";
        }
        else if (sum(1) > hoverThreshold)
        {
            // Color Manipulation
            GameObject.Find("ColorOptions").GetComponent<ColorOptions>().highlight();
            inUse = "middle";
        }
        else if (sum(0) > hoverThreshold)
        {
            // Shade Manipulation
            GameObject.Find("ShadeSphere").GetComponent<UpdateSphereColor>().highlight();
            inUse = "index";
        }
        else
        {
            if (inUse == "pinky")
            {
                Debug.Log("Pinky off");
                GameObject.Find("Knob").GetComponent<KnobControl>().unhighlight();
            }
            else if (inUse == "middle")
            {
                GameObject.Find("ColorOptions").GetComponent<ColorOptions>().unhighlight();
                Debug.Log("middle off");
            }
            else if (inUse == "ring")
            {
                Debug.Log("ring off");
                GameObject.Find("ScaleCube").GetComponent<ScaleIronman>().unhighlight();
            }
            else if (inUse == "index")
            {
                Debug.Log("index off");
                GameObject.Find("ShadeSphere").GetComponent<UpdateSphereColor>().unhighlight();
            }
            inUse = "None";
        }
    }
    private int sum(int finger)
    {
        int sum = 0;
        for (int i = 0; i < fingerSums[finger].Length; i++)
        {
            sum += fingerSums[finger][i];
        }
        return sum;
    }
    public string getInUse()
    {
        return inUse;
    }
}
