using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionGauge : MonoBehaviour
{

    public PlayerSoul soul;

    private static bool isCW;
    private static int speed;
    private static bool isSpace;
    private static bool isMove;

    public static Quaternion rotation;

    
    // Use this for initialization
    void Start()
    {
        isCW = false;
        speed = 5;
        isMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSpace = true;
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            isSpace = false;
            isMove = false;
            soul.returnToGauge(transform.position);
        }

        if (isSpace && !isMove)
        {
            // print(transform.eulerAngles.z);
            // print(Mathf.Sin(transform.eulerAngles.z));
            isMove = true;
            soul.Move(new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.PI / 180),Mathf.Sin(transform.eulerAngles.z * Mathf.PI / 180)));
            return;
        }
        // print(transform.eulerAngles.z* Mathf.PI / 180);

        /*
         Check when to turn back
         */
        if (transform.eulerAngles.z >= 180 && !isCW)
        {
            isCW = true;
        }
        else if (transform.eulerAngles.z >= 180 && isCW)
        {
            isCW = false;
        }

        if (isCW)
        {
           transform.Rotate(Vector3.forward, -speed, Space.World);
        }
        else
        {
           transform.Rotate(Vector3.forward, speed, Space.World);
        }
    }

    Vector2 getPos()
    {
        return transform.position;
    }

    void OnCollisionEnter2D(Collision2D other)
    {


        Debug.Log(other.gameObject.name);
        // Collision only Wall object
        if (other.gameObject.tag != "Wall")
        {
            return;
        }
        
        if (isCW)
        {
            // print("in");
            isCW = false;
        }
        else
        {
            isCW = true;
        }
    }

}

