using UnityEngine;
using System.Collections;

public class DynamicSkybox : MonoBehaviour {
    float z = 0;
    // Use this for initialization
	void Start () {
        //StartCoroutine(toSpaaace());
	}
	
	// Update is called once per frame
	void Update () {
        z += 0.001f;
        RenderSettings.skybox.SetVector("_Center", new Vector3(100, 100, z));
    }/*
    IEnumerator toSpaaace(float time)
    {
        while (alpha < 1f)
        {
            alpha = Mathf.Lerp(0f, 1f, (Time.time - startTime) / duration);
            blinkColor.a = alpha;
            material.SetColor("_BlinkColor", blinkColor);
            yield return null;
        }
        startTime = Time.time;
        while (alpha > 0f)
        {
            alpha = Mathf.Lerp(1f, 0f, (Time.time - startTime) / duration);
            blinkColor.a = alpha;
            material.SetColor("_BlinkColor", blinkColor);
            yield return null;
        }
    }*/
}
