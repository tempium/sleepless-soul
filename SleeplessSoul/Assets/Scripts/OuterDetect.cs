using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterDetect : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 newVector = transform.position - other.gameObject.transform.position;
        other.gameObject.GetComponent<Rigidbody2D>().velocity = newVector;
    }

}
