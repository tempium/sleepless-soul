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
        if (other.CompareTag("Player") && !(playerSoul.IsOut() && playerSoul.body.Equals(transform.parent.gameObject)) && !playerSoul.isPossess) {
            anim.SetBool("IsPossess", true);
            pull = true;

            other.GetComponent<PlayerSoul>().Possessing(transform.parent.gameObject);
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
