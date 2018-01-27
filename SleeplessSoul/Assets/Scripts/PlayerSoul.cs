using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoul : MonoBehaviour {

    public float moveSpeed = 1;

    private int limitedSpeed = 3;

    private Rigidbody2D rb;
    private GameObject body;
    private Animator anim;

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
	}

    void Update()
    {
        PulltoGauge();
       
    }


    void FixedUpdate () {
        /*if (Input.GetKeyDown(KeyCode.Space)) {
            Vector2 direction = new Vector2(Input.mousePosition.x - transform.position.x, Input.mousePosition.y - transform.position.y);
            Move(direction.normalized);
        }
    
        else if (rb.velocity.sqrMagnitude == 0) {
            anim.SetBool("IsMove", false);
        }*/
        if (rb.velocity.sqrMagnitude == 0)
        {
            anim.SetBool("IsMove", false);
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
        anim.SetBool("IsMove", true);
        rb.velocity = direction * moveSpeed;
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
}
