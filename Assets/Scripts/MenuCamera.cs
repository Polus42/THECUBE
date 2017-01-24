using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class MenuCamera : MonoBehaviour {
    Transform _player;
    bool _isthereCoroutine = false;
	// Use this for initialization
	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(GameObject.Find("Title").transform);
        if (SpectrumAnalyzer.timeSinceLastBeat(0)<0.1)
        {
            StopAllCoroutines();
            StartCoroutine(ZoomIn(0.1f, 20));
            int r = Random.Range(1,4);
            switch (r)
            {
                case 1:
                    StartCoroutine(ZoomIn(0.1f, 20));
                    break;
                case 2:
                    StartCoroutine(MoveSlow(10000, GameObject.Find("CameraPos1").transform.position));
                    break;
                case 3:
                    StartCoroutine(MoveSlow(10000, GameObject.Find("CameraPos2").transform.position));
                    break;
            }
        }
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            Play();
        }
    }
    IEnumerator MoveSlow(float delayTime, Vector3 pos)
    {
        float startTime = Time.time; // Time.time contains current frame time, so remember starting point
        while (Time.time - startTime <= delayTime)
        { // until one second passed
            transform.position = Vector3.Lerp(transform.position, pos, Time.time - startTime); // lerp from A to B in one second
            yield return 1; // wait for next frame
        }
    }
    // back and forth
    IEnumerator ZoomIn(float delayTime, float distance)
    {
        float startTime = Time.time; // Time.time contains current frame time, so remember starting point
        Vector3 pos = transform.position + transform.forward * distance;
        while (Time.time - startTime <= delayTime/2)
        { // until one second passed
            transform.position = Vector3.Lerp(transform.position, pos, Time.time - startTime); // lerp from A to B in one second
            yield return 1; // wait for next frame
        }
        pos = transform.position - transform.forward * distance;
        startTime = Time.time;
        while (Time.time - startTime <= delayTime/2)
        { // until one second passed
            transform.position = Vector3.Lerp(transform.position, pos, Time.time - startTime); // lerp from A to B in one second
            yield return 1; // wait for next frame
        }
    }
    void Play()
    {
        SceneManager.LoadScene("main", LoadSceneMode.Single);
    }
}
