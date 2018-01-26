using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterDetect : MonoBehaviour {

    private Vector2 newVector;

    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        newVector = transform.position - other.gameObject.transform.position;
        other.gameObject.GetComponent<Rigidbody2D>().velocity = newVector;
 
    }
}
