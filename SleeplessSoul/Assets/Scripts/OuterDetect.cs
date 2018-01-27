using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterDetect : MonoBehaviour
{

    private bool pull = false;
    private Animator anim;

    private void Start() {
        anim = transform.parent.GetComponentInChildren<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            anim.SetBool("IsPossess", true);
            pull = true;

            other.GetComponent<PlayerSoul>().Possessing(transform.parent.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (pull) {
            Vector2 newVector = transform.position - collision.gameObject.transform.position;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = newVector.normalized * 2;
        }
    }


    private void OnTriggerExit2D(Collider2D collision) {
        
    }

    public void stopPull() {
        pull = false;
    }

}
