using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoul : MonoBehaviour {

    public float moveSpeed = 1;

    private Rigidbody2D rb;
    private GameObject body;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Vector2 direction = new Vector2(Input.mousePosition.x - transform.position.x, Input.mousePosition.y - transform.position.y);
            Move(direction.normalized);
        }
        
        // [Kris] Manual controls for testing collision with Firewall
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2 direction = Vector2.down;
            Move(direction.normalized);
        }
        
	}

    void Move(Vector2 direction) {
        rb.velocity = direction * moveSpeed;
    }
}
