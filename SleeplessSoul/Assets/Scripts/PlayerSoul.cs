using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoul : MonoBehaviour {

	public float moveSpeed = 1;
	public float timeThreshold = 1;

	private int limitedSpeed = 3;

	private Rigidbody2D rb;
	public GameObject body;
	private Animator anim;
	private SpriteRenderer render;

	public bool isMove = false;
	public bool isPossess = false;
	public bool isOut = false;
	public bool isInside = false;
	private float timer;

    public bool isPullBack;
    public bool isPullCurse;
    public bool isAtGauge;
    //public bool test;

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
            float angle = Vector2.SignedAngle(new Vector2(0, 1), (transform.position - body.transform.position));
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, -angle, 1-closeness));
        }

        if (isOut) {
            if (body == null) {
                anim.SetBool("IsOut", false);
                isOut = false;
                return;
            }
            float radius = body.GetComponentInChildren<OuterDetect>().GetComponent<CircleCollider2D>().radius;
            float closeness = (transform.position - body.transform.position).magnitude / radius;
            if (closeness > 1) {
                anim.SetBool("IsOut", false);
                isOut = false;
                return;
            }

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
        //if (!isPullCurse && isPullBack && Mathf.Abs(DirectionGauge.pos.x - transform.position.x) < 0.5 && Mathf.Abs(DirectionGauge.pos.y - transform.position.y) < 0.5) { 
        //    Physics2D.IgnoreLayerCollision(1, 0, false);
        //    isPullBack = false;
        //    isAtGauge = true;

        //}
        //if (isAtGauge) { 
        //    transform.position = new Vector2(DirectionGauge.pos.x, DirectionGauge.pos.y);
        //}
    }

    public Vector2 getVelocity()
    {
        return rb.velocity;
    }

    public void Move(Vector2 direction) {
        if (isMove || isPossess) {
            return;
        }
        //direction = new Vector2(0, 1);

        anim.SetBool("IsOut", true);
        isOut = true;
        anim.SetBool("IsMove", true);
        isMove = true;
        isInside = false;
        timer = Time.time;
        rb.velocity = direction.normalized * moveSpeed;

        if (body != null) {
            body.GetComponent<CursedObject>().Release();
        }
        GetComponent<Collider2D>().enabled = true;
        GetComponentInChildren<SpriteRenderer>().enabled = true;
    }

    public void Stop() { 
        anim.SetBool("IsMove", false);
        isMove = false;
        if (isOut) {
            anim.SetBool("IsOut", false);
            isOut = false;
            body.GetComponentInChildren<OuterDetect>().StartPull();
            Possessing(body);
        }
        rb.velocity = new Vector2(0, 0);

        if (!isInside && !isPossess) {
            ReturnToGauge(DirectionGauge.reference.transform.position);
            //test = true;
        }
    }

    public void Possessing(GameObject body) {
        isMove = false;
        isPossess = true;
        anim.SetBool("IsOut", false);
        isOut = false;
        
        anim.SetBool("IsPossess", true);
        anim.speed = 2f / (transform.position - body.transform.position).magnitude;
        this.body = body;
    }

    public void Possess() {
        isPossess = false;
        isMove = false;
        isInside = true;
        anim.SetBool("IsPossess", false);
        transform.rotation = Quaternion.identity;
        Stop();

        //transform.position = body.transform.position;
        DirectionGauge.reference = body;

        GetComponent<Collider2D>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    public bool StopAndNoCurse()
    {
        return !isInside && body == null;
    }

    public void ReturnToGauge(Vector2 pos)
    {
        Physics2D.IgnoreLayerCollision(1, 0, true);

        float usX = pos.x - transform.position.x;
        float usY = pos.y - transform.position.y;

        Move(new Vector2(usX, usY));
        isPullBack = true;
        //test = false;
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

    public bool IsMove() {
        return isMove;
    }
    public bool IsPossess() {
        return isPossess;
    }
    public bool IsInside() {
        return isInside;
    }
    public bool IsOut() {
        return isOut;
    }
}
