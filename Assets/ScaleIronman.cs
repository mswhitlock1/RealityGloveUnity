using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleIronman : MonoBehaviour {

    private bool highlighted;
	// Use this for initialization
	void Start () {
        highlighted = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (highlighted)
        {
            float scale = 0.0025f * Mathf.Sin(3 * Time.time) + 0.0225f;
            GameObject.Find("ScaleCanvas").transform.localScale = new Vector3(scale, scale, scale);
        }
    }
    public void updateScale(float raw_delta)
    {
        float delta = raw_delta / 500;
        if (transform.localScale.x + delta < 0)
        {
            return;
        }
        transform.localScale += new Vector3(delta, delta, delta);
        GameObject.Find("Ironman").transform.localScale = GameObject.Find("Ironman").transform.localScale + new Vector3(25 * delta, 25 * delta, 25 * delta);
    }

    public void highlight()
    {
        highlighted = true;
    }
    public void unhighlight()
    {
        GameObject.Find("ScaleCanvas").transform.localScale = new Vector3(0.015f, 0.015f, 0.015f);
        highlighted = false;
    }
}
