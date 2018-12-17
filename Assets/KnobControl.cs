using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnobControl : MonoBehaviour {
    private float currentAngle;
    public GameObject tick;
    private bool highlighted;

    //public GameObject VolumeCanvas;
	// Use this for initialization
	void Start () {
        currentAngle = -90;
        highlighted = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (highlighted)
        {
            float scale = 0.0025f * Mathf.Sin(3 * Time.time) + 0.0225f;
            GameObject.Find("RotationCanvas").transform.localScale = new Vector3(scale, scale, scale);
        }
	}
    public void knobDelta(float delta)
    {
        /*if (!VolumeCanvas.transform.Find("Text").GetComponent<VolumeAdjustment>().updateVolume(delta))
        {
            VolumeCanvas.transform.Find("Text").GetComponent<VolumeAdjustment>().updateVolume(90 - currentAngle);
            return;
        }*/
        currentAngle = (currentAngle + delta) % 360;
        updateTickOrientation();
    }
    public void updateTickOrientation()
    {
        tick.transform.localEulerAngles = new Vector3(90, 0, 90 + currentAngle);
        tick.transform.localPosition = new Vector3(tick.transform.localScale.y * Mathf.Cos(currentAngle * Mathf.Deg2Rad), 0,
                                                   tick.transform.localScale.y * Mathf.Sin(currentAngle * Mathf.Deg2Rad));
        GameObject.Find("Ironman").transform.localEulerAngles = new Vector3(0, 180, currentAngle + 90);
    }

    public void highlight()
    {
        highlighted = true;
    }
    public void unhighlight()
    {
        highlighted = false;
        GameObject.Find("RotationCanvas").transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);
    }
}
