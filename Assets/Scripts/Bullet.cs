using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>()!=null)
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0,1000,0));
        }
    }
}
