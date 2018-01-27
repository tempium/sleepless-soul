using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firewall : MonoBehaviour {

    public float speed = 1;

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
        transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Destroyed Object");
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Object")
        {
            Destroy(other.gameObject);
        }
    }
    
}
