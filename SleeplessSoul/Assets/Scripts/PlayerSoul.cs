using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoul : MonoBehaviour {

    public float moveSpeed = 1;
	public float timeThreshold = 1;

	private Rigidbody2D rb;
	private Animator anim;
	private SpriteRenderer render;

	public GameObject linkedCursedObject;
    private float distanceToLinkedCursedObject;
    private float outerRadiusOfLinkedCursedObject;

    // AudioPlayer for SFX related to PlayerSoul
    private PlayerSoulSfxPlayer sfxPlayer;
    
	private float timer;

    public bool isPullBack;
    public bool isPullCurse;
    public bool isAtGauge;
    //public bool test;

    // Describes player state
    public enum PlayerState
    {
        INITIAL,    // Initial spawned state. Must not be set again after initial set.
        POSSESS,    // Stationary in a cursed object
        DEPART,     // Moving away from cursed object (inside force field)
        FLOAT,      // Free floating away from any force field
        ARRIVE,     // Being sucked in by a cursed object (inside force field)
        RETURN,     // Return to last cursed object because it hit a wall / stopped
        DEFLECT     // Moving away from holy object. [UNUSED]
    }
    public PlayerState playerState = PlayerState.INITIAL;

    // Use this for initialization
    void Start () {
        isPullBack = false;
        isPullCurse = false;
        isAtGauge = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        render = GetComponentInChildren<SpriteRenderer>();
        sfxPlayer = GetComponent<PlayerSoulSfxPlayer>();
    }

    void Update()
    {
        Debug.Log(playerState);
        // Update distance to linked cursed object
        distanceToLinkedCursedObject = (transform.position - linkedCursedObject.transform.position).magnitude;
        outerRadiusOfLinkedCursedObject = linkedCursedObject.GetComponentInChildren<OuterDetect>().GetComponent<CircleCollider2D>().radius;


        // Handle state change DEPART -> FLOAT (In Addition To linkedCursedObject handle)
        if (distanceToLinkedCursedObject > outerRadiusOfLinkedCursedObject && playerState == PlayerState.DEPART)
        {
            playerState = PlayerState.FLOAT;
        }

        // Handle animation of alpha value for arrive
        if (playerState == PlayerState.ARRIVE) {
            float closeness = Mathf.Clamp01(distanceToLinkedCursedObject / outerRadiusOfLinkedCursedObject);
            render.color = new Color(1, 1, 1, closeness);
            transform.localScale = new Vector3(closeness, closeness, transform.localScale.z);
        }

        // Handle animation of alpha value for depart
        if (playerState == PlayerState.DEPART) {
            
            float closeness = Mathf.Clamp01(distanceToLinkedCursedObject / outerRadiusOfLinkedCursedObject);
            render.color = new Color(1, 1, 1, closeness);
            transform.localScale = new Vector3(closeness, closeness, transform.localScale.z);
        }
    }

    void FixedUpdate () {
        if (playerState == PlayerState.FLOAT) {
            if (rb.velocity.sqrMagnitude == 0 || Time.time - timer > timeThreshold) {
                // FLOAT -> RETURN (1)
                Debug.Log("Zero Speed");
                playerState = PlayerState.RETURN;
            }
        }

        if (playerState == PlayerState.RETURN)
        {
            ReturnToLinkedCursedObject();
        }
    }

    public Vector2 getVelocity()
    {
        return rb.velocity;
    }

    // Move to specified direction (bi-directional)
    public void Move(Vector2 direction) {
        
        /*
        // Prevent multiple space-bar by user
        if (isMove || isPossess) {
            return;
        }

    */
        anim.SetBool("IsMove", true);
        timer = Time.time;
        rb.velocity = direction.normalized * moveSpeed;

        if (linkedCursedObject != null) {
            linkedCursedObject.GetComponent<CursedObject>().Release();
        }

        // Make player visible (after in POSSESS state)
        GetComponent<Collider2D>().enabled = true;
        GetComponentInChildren<SpriteRenderer>().enabled = true;

        // Change PlayerState to DEPART
        if (playerState == PlayerState.POSSESS)
        {
            playerState = PlayerState.DEPART;
        }

        // Play Depart SFX
        sfxPlayer.PlayDepartSfx();
    }

    // Return to linked cursed object
    // Any State can return
    public void StopMovement() {
        
        anim.SetBool("IsMove", false);
        rb.velocity = new Vector2(0, 0);
        /*
        if (isOut) {
            isOut = false;
            linkedCursedObject.GetComponentInChildren<OuterDetect>().StartPull();
            ArriveAt(linkedCursedObject);
        }
        */
    }

    // Called once on arriving inside outer boundary of cursed object. Called by outer radius of target object.
    // target object will drag this player in. The player is responsible for animations (e.g. transparent).
    // All states can transit to ARRIVE state
    public void ArriveAt(GameObject targetCursedObject) {
        StopMovement();
        Debug.Log("Called Arrive At");
        playerState = PlayerState.ARRIVE;
        
        anim.SetBool("IsPossess", true);
        this.linkedCursedObject = targetCursedObject;

        sfxPlayer.PlayPossessSfx();
    }

    // Transition to POSSESS state
    public void Possess() {

        // Only ARRIVE state can call this method
        Debug.Assert(playerState == PlayerState.ARRIVE, "Expected player in ARRIVE state");

        playerState = PlayerState.POSSESS;
        
        anim.SetBool("IsPossess", false);
        // ReturnToLinkedCursedObject();

        //transform.position = body.transform.position;
        DirectionGauge.reference = linkedCursedObject;

        // Make player invisible in POSSESS state
        GetComponent<Collider2D>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    
    //
    public void ReturnToLinkedCursedObject()
    {

        Debug.Log("Called ReturnToLinkedObject");
        Physics2D.IgnoreLayerCollision(1, 0, true);

        // Make Outer
        linkedCursedObject.GetComponentInChildren<OuterDetect>().StartPull();

        // Disable Velocity and apply manual movement
        StopMovement();
        Vector2 movementVector = linkedCursedObject.transform.position - transform.position;
        
        Debug.Log(movementVector);
        Move(movementVector);
        isPullBack = true;
        //test = false;
    }
    
    /// Collision Handlers ///

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Wall")) {
            Debug.Log("Hit Wall"); 
            StopMovement();
            playerState = PlayerState.RETURN;
            // FLOAT -> RETURN (2)
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

    /// Getters ///

    public bool IsMove() {
        return playerState == PlayerState.DEPART || playerState == PlayerState.FLOAT || playerState == PlayerState.RETURN;
    }
    public bool IsPossess() {
        return playerState == PlayerState.DEPART;
    }
    public bool IsInside() {
        return playerState == PlayerState.POSSESS;
    }
    public bool IsOut() {
        return playerState == PlayerState.ARRIVE;
    }
}
