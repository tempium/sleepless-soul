using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerDetect : MonoBehaviour {

    private PlayerSoul soul;

    void OnTriggerEnter2D(Collider2D other)
    { 
        other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //Detect pull to the Curse

    }
}
