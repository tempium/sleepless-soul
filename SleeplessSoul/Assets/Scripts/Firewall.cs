using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firewall : MonoBehaviour {

    public GameObject firewallObject;

    [Range (0, 3.6f)]
    public float collisionOffsetFromTop;

	// Use this for initialization
	void Start () {

        BoxCollider2D bc = GetComponent<BoxCollider2D>();
        bc.size = new Vector2(7.2f, 3.6f - collisionOffsetFromTop);
        bc.offset = new Vector2(0.0f, -collisionOffsetFromTop / 2);
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Destroyed Object");
        Destroy(other.gameObject);
    }
    
}
