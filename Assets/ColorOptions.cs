using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorOptions : MonoBehaviour {
    public Material unselected;
    public Material orange;
    public Material red;
    public Material blue;
    public Material purple;

    private GameObject orangeButton;
    private GameObject redButton;
    private GameObject blueButton;
    private GameObject purpleButton;

    private bool highlighted;

    // Use this for initialization
    void Start () {
        orangeButton = transform.Find("OrangeButton").gameObject;
        redButton = transform.Find("RedButton").gameObject;
        blueButton = transform.Find("BlueButton").gameObject;
        purpleButton = transform.Find("PurpleButton").gameObject;
        updateColor("None");
        highlighted = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (highlighted)
        {
            float scale = 0.0025f * Mathf.Sin(3 * Time.time) + 0.0225f;
            GameObject.Find("ColorCanvas").transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public void updateColor(string color)
    {
        orangeButton.GetComponent<Renderer>().material = unselected;
        redButton.GetComponent<Renderer>().material = unselected;
        blueButton.GetComponent<Renderer>().material = unselected;
        purpleButton.GetComponent<Renderer>().material = unselected;

        if (color == "orange")
        {
            orangeButton.GetComponent<Renderer>().material = orange;
            GameObject.Find("IronmanHelmet").GetComponent<Renderer>().material = orange;
        }
        else if (color == "red")
        {
            redButton.GetComponent<Renderer>().material = red;
            GameObject.Find("IronmanHelmet").GetComponent<Renderer>().material = red;
        }
        else if (color == "blue")
        {
            blueButton.GetComponent<Renderer>().material = blue;
            GameObject.Find("IronmanHelmet").GetComponent<Renderer>().material = blue;
        }
        else if (color == "purple")
        {
            purpleButton.GetComponent<Renderer>().material = purple;
            GameObject.Find("IronmanHelmet").GetComponent<Renderer>().material = purple;
        }
    }

    public void highlight()
    {
        highlighted = true;
    }
    public void unhighlight()
    {
        highlighted = false;
        GameObject.Find("ColorCanvas").transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);
    }
}
