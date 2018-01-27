using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firewall : MonoBehaviour {

    public float speed = 1;

    public GameObject firewallObject;

    //[Range (0, 3.6f)]
    //public float collisionOffsetFromTop;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
		Debug.Log (other.gameObject.name);
        Destroy(other.gameObject);
    }
    
}
