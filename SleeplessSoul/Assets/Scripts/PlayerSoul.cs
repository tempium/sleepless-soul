using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoul : MonoBehaviour {

    public float moveSpeed = 1;
    public float timeThreshold = 1;

    private Rigidbody2D rb;
    private GameObject body;
    private Animator anim;

    private bool isMove = false;
    private float timer;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
	}
	
	void FixedUpdate () {
        //if (Input.GetKeyDown(KeyCode.Space)) {
        //    Vector2 direction = new Vector2(Input.mousePosition.x - transform.position.x, Input.mousePosition.y - transform.position.y);
        //    Move(direction.normalized);
        //}

        if (isMove) {
            if (rb.velocity.sqrMagnitude == 0 || Time.time - timer > timeThreshold) {
                Stop();
            }
        }
	}

    public void Move(Vector2 direction) {
        if (isMove) {
            return;
        }
        //direction = new Vector2(0, 1);

        anim.SetBool("IsMove", true);
        isMove = true;
        timer = Time.time;
        rb.velocity = direction * moveSpeed;
    }

    public void Stop() { 
        anim.SetBool("IsMove", false);
        isMove = false;
        rb.velocity = new Vector2(0, 0);
    }

    public void returnToGauge(Vector2 pos)
    {
        transform.position = pos;
        rb.velocity = new Vector2(0, 0);
    }
    
    private void OnColissionEnter2D(Collider2D other) { 
        if (other.CompareTag("Wall")) { 
            Stop();
        }
    }
}
