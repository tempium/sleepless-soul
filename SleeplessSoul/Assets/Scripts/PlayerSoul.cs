using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoul : MonoBehaviour {

    public float moveSpeed = 1;
    public float timeThreshold = 1;

    private int limitedSpeed = 3;

    private Rigidbody2D rb;
    private GameObject body;
    private Animator anim;
    private SpriteRenderer render;

    private bool isMove = false;
    private bool isPossess = false;
    private float timer;

    public CursedObject cursedObject;

    public bool isPullBack;
    public bool isPullCurse;
    public bool isAtGauge;

	// Use this for initialization
	void Start () {
        isPullBack = false;
        isPullCurse = false;
        isAtGauge = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        render = GetComponentInChildren<SpriteRenderer>();
	}

    void Update()
    {
        PulltoGauge();
        if (isPossess) {
            float radius = body.GetComponentInChildren<OuterDetect>().GetComponent<CircleCollider2D>().radius;
            float closeness = Mathf.Clamp01((transform.position - body.transform.position).magnitude / radius);
            render.color = new Color(1, 1, 1, closeness);
            transform.localScale = new Vector3(closeness, closeness, transform.localScale.z);
        }
    }

    void FixedUpdate () {
        if (isMove) {
            if (rb.velocity.sqrMagnitude == 0 || Time.time - timer > timeThreshold) {
                Stop();
            }
        }
    }

    void PulltoGauge()
    {
        if (!isPullCurse && isPullBack && Mathf.Abs(DirectionGauge.pos.x - transform.position.x) < 0.5 && Mathf.Abs(DirectionGauge.pos.y - transform.position.y) < 0.5)
        {
            Physics2D.IgnoreLayerCollision(1, 0, false);
            isPullBack = false;
            isAtGauge = true;

        }
        if (isAtGauge)
        {
            Move(new Vector2(0, 0));
            transform.position = new Vector2(DirectionGauge.pos.x, DirectionGauge.pos.y);
        }
    }

    public Vector2 getVelocity()
    {
        return rb.velocity;
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

    public bool StopAndNoCurse()
    {
        if(Mathf.Sqrt(Mathf.Abs(Mathf.Pow(rb.velocity.x,2) - Mathf.Pow(rb.velocity.y, 2))) < 0.1 && !isPullCurse && !isAtGauge)
            //Detect pulling to the Gauge
        return true;
        return false;
    }

    public void returnToGauge(Vector2 pos)
    {
        Physics2D.IgnoreLayerCollision(1, 0, true);

        float usX = pos.x - transform.position.x;
        float usY = pos.y - transform.position.y;

        Move(new Vector2(usX,usY));
        isPullBack = true;
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
