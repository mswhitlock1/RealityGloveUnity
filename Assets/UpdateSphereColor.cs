using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSphereColor : MonoBehaviour {
    private float colorValue;

    private bool highlighted;
    // Use this for initialization
    void Start () {
        colorValue = 0.5f;
        highlighted = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        updateRendering();

        if (highlighted)
        {
            float scale = 0.0025f * Mathf.Sin(3 * Time.time) + 0.0225f;
            GameObject.Find("BrightnessCanvas").transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public void updateColor(float delta)
    {
        colorValue += (delta / 200f);
        if (colorValue > 1) colorValue = 1;
        if (colorValue < 0) colorValue = 0;
    }

    private void updateRendering()
    {
        GetComponent<Renderer>().material.SetColor("_Color", new Color(colorValue, colorValue, colorValue));
        GameObject.Find("Ironman").GetComponent<Renderer>().material.SetColor("_Color", new Color(colorValue, colorValue, colorValue));
    }

    public void highlight()
    {
        highlighted = true;
    }
    public void unhighlight()
    {
        highlighted = false;
        GameObject.Find("BrightnessCanvas").transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);
    }
}
