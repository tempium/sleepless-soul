using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterDetect : MonoBehaviour
{

    private PlayerSoul playerSoul;

    private Animator anim;
    private bool pull = false;

    private void Start() {
        anim = transform.parent.GetComponentInChildren<Animator>();
        playerSoul = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSoul>();
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && 
            !(playerSoul.playerState == PlayerSoul.PlayerState.DEPART && playerSoul.linkedCursedObject.Equals(transform.parent.gameObject)) && 
            !(playerSoul.playerState == PlayerSoul.PlayerState.ARRIVE)) {
            anim.SetBool("IsPossess", true);
            pull = true;
            Debug.Log("OnTriggerEnter2D in OuterDetect");
            other.gameObject.GetComponent<PlayerSoul>().ArriveAt(transform.parent.gameObject);
        }
    }

    private void FixedUpdate() {
        if (pull) { 
            Vector2 newVector = transform.position - playerSoul.transform.position;
            playerSoul.gameObject.GetComponent<Rigidbody2D>().velocity = newVector.normalized * 2;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision.CompareTag("Player") && pull) {
        //    Vector2 newVector = transform.position - collision.gameObject.transform.position;
        //    collision.gameObject.GetComponent<Rigidbody2D>().velocity = newVector.normalized * 2;
        //}
    }


    private void OnTriggerExit2D(Collider2D collision) {
        if (playerSoul.IsOut()) {
            
        }
    }

    public void StopPull() {
        pull = false;
    }

    public void StartPull() {
        pull = true;
    }

    public bool IsPull() {
        return pull;
    }
}
