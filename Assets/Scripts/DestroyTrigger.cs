using UnityEngine;
using System.Collections;

public class DestroyTrigger : MonoBehaviour {
    public int recyclingDistance = 100;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Ground")
        {
            other.gameObject.SendMessage("resetColor");
            other.transform.Translate(0, 0, recyclingDistance);
        }
        else
        {
            Destroy(other.gameObject);
        }
        
    }
}
