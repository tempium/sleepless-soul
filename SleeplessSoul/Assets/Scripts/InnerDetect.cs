using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerDetect : MonoBehaviour {

    private PlayerSoul playerSoul;

    private void Start() {
        playerSoul = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSoul>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!playerSoul.isPossess) {
            return;
        }
        other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (other.CompareTag("Player")) {
            other.GetComponent<PlayerSoul>().Possess();
            transform.parent.GetComponentInChildren<OuterDetect>().StopPull();
        }
    }
}
