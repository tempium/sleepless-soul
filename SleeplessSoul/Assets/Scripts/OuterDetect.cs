using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterDetect : MonoBehaviour
{
    public PlayerSoul soul;

    void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 newVector = (transform.position - other.gameObject.transform.position)*2;
        other.gameObject.GetComponent<Rigidbody2D>().velocity = newVector;
        if ((PlayerSoul)other.gameObject.GetComponent<PlayerSoul>() == soul && Mathf.Sqrt((Mathf.Pow(transform.position.x - DirectionGauge.pos.x, 2)) + (Mathf.Pow(transform.position.y - DirectionGauge.pos.y, 2))) > 0.1)
        {
            //Detect will be pulled to the Curse
            soul.isPullCurse = true;
        }
    }

}
