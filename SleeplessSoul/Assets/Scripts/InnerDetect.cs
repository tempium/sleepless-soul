using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerDetect : MonoBehaviour {

    private PlayerSoul playerSoul;
    private OuterDetect outerDetect;

    private void Start() {
        playerSoul = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSoul>();
        outerDetect = transform.parent.GetComponentInChildren<OuterDetect>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Inner Triggered");
        /*
        if (playerSoul.isOut || (playerSoul.isPossess && !outerDetect.IsPull())) {
            return;
        }
        */
        Debug.Log(other.gameObject.name);
        other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (other.CompareTag("Player")) {
            playerSoul.Possess();
            outerDetect.StopPull();
        }
    }
}
