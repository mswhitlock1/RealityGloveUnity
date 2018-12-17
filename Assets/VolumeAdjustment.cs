using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeAdjustment : MonoBehaviour {

    // Use this for initialization
    private float volumeVal;
	void Start ()
    {
        volumeVal = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public bool updateVolume(float delta)
    {
        if (volumeVal + delta > 360 || volumeVal + delta < 0) return false;
        volumeVal += delta;
        GetComponent<Text>().text = "VOLUME: " + ((int)(volumeVal / 24)).ToString();
        return true;
    }

    public float getVolume()
    {
        return volumeVal;
    }
}
