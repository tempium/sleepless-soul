using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoul : MonoBehaviour {

    public float moveSpeed = 1;
    public float timeThreshold = 1;

    private Rigidbody2D rb;
    private GameObject body;
    private Animator anim;
    private SpriteRenderer render;

    private bool isMove = false;
    private bool isPossess = false;
    private float timer;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        render = GetComponentInChildren<SpriteRenderer>();
	}

    private void Update() {
        if (isPossess) {
            float radius = body.GetComponentInChildren<OuterDetect>().GetComponent<CircleCollider2D>().radius;
            float closeness = Mathf.Clamp01((transform.position - body.transform.position).magnitude / radius);
            render.color = new Color(1, 1, 1, closeness);
            transform.localScale = new Vector3(closeness, closeness, transform.localScale.z);
        }
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

        body.GetComponent<CursedObject>().Release();
        GetComponent<Collider2D>().enabled = true;
        GetComponentInChildren<SpriteRenderer>().enabled = true;
    }

    public void Stop() { 
        anim.SetBool("IsMove", false);
        isMove = false;
        rb.velocity = new Vector2(0, 0);
    }

    public void Possessing(GameObject body) {
        isPossess = true;
        anim.SetBool("IsPossess", true);
        this.body = body;
    }

    public void Possess() {
        isPossess = false;
        anim.SetBool("IsPossess", false);
        Stop();

        GetComponent<Collider2D>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    public void returnToGauge(Vector2 pos)
    {
        transform.position = pos;
        rb.velocity = new Vector2(0, 0);
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Wall")) { 
            Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cursed")) {

        }
    }

    //private void OnCollisionStay2D(Collision2D other) { 
    //    if (other.gameObject.CompareTag("Wall")) { 
    //        Stop();
    //    }
    //}
}
