using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoul : MonoBehaviour {

    public float moveSpeed = 1;

    private Rigidbody2D rb;
    private GameObject body;
    private Animator anim;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
	}
	
	void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Vector2 direction = new Vector2(Input.mousePosition.x - transform.position.x, Input.mousePosition.y - transform.position.y);
            Move(direction.normalized);
        }
    
        else if (rb.velocity.sqrMagnitude == 0) {
            anim.SetBool("IsMove", false);
        }
	}

    void Move(Vector2 direction) {
<<<<<<< HEAD
        anim.SetBool("IsMove", true);
        rb.velocity = direction * moveSpeed;
    }
=======
        anim.SetBool("IsMove", true);`
        rb.velocity = direction * moveSpeed;
    }

    public void returnToGauge(Vector2 pos)
    {
        transform.position = pos;
        rb.velocity = new Vector2(0, 0);
    }
>>>>>>> master
}
