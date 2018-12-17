using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ReadFingerInput : MonoBehaviour {
    private int[][] fingerSums;

    private int hoverThreshold = 10;
    private int pressThreshold = 40;
    private int rotatingIndex = 0;

    enum FingerType { index, middle, ring, pinky };
    private bool[] coolingDown;
    private float[] pressStartTime;

    [Tooltip("Material 0: Unpressed; Material 1: Hover; Material 2: Pressed")]
    public Material[] materials;

    void Awake()
    {
        fingerSums = new int[4][];
        fingerSums[0] = new int[100];
        fingerSums[1] = new int[100];
        fingerSums[2] = new int[100];
        fingerSums[3] = new int[100];
        coolingDown = new bool[4];
        pressStartTime = new float[4];
    }
    // Invoked when a line of data is received from the serial device.
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
        rotatingIndex = (rotatingIndex + 1) % 100;
    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }
    
    void checkFinger(int fingerNum)
    {
        if (coolingDown[fingerNum])
        {
            if (fingerSums[fingerNum].Sum() < hoverThreshold)
            {
                coolingDown[fingerNum] = false;
            }
            else if (pressStartTime[fingerNum] + 1 < Time.time)
            {
                transform.Find("Option" + (fingerNum + 1).ToString()).GetComponent<Renderer>().material = materials[0];
                GameObject.Find("Option" + (fingerNum + 1).ToString() + "Canvas/Text").GetComponent<Text>().color = Color.black;
            }
        }
        else
        {
            if (fingerSums[fingerNum].Sum() > pressThreshold)
            {
                coolingDown[fingerNum] = true;
                pressStartTime[fingerNum] = Time.time;
                transform.Find("Option" + (fingerNum + 1).ToString()).GetComponent<Renderer>().material = materials[2];
                GameObject.Find("Option" + (fingerNum + 1).ToString() + "Canvas/Text").GetComponent<Text>().color = Color.white;

            }
            else if (fingerSums[fingerNum].Sum() > hoverThreshold)
            {
                transform.Find("Option" + (fingerNum + 1).ToString()).GetComponent<Renderer>().material = materials[1];
            }
            else
                transform.Find("Option" + (fingerNum + 1).ToString()).GetComponent<Renderer>().material = materials[0];

        }
    }
}
