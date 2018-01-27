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
    public static Vector2 pos;

   
    // Use this for initialization
    void Start()
    {
        isCW = false;
        isMove = false;

        speed = 5;

        Physics2D.IgnoreLayerCollision(5, 0, true);
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        SoulCommand();
        Rotate();

    }

    void SoulCommand()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMove)
        {
            print("in");
            isSpace = true;
            soul.isAtGauge = false;
        }
        else if (Input.GetKeyDown(KeyCode.Return) || soul.StopAndNoCurse())
        {
            isSpace = false;
            isMove = false;
            soul.returnToGauge(transform.position);
        }

        if (isSpace && !isMove)
        {
            isMove = true;
            soul.Move(new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.PI / 180), Mathf.Sin(transform.eulerAngles.z * Mathf.PI / 180)));
            return;
        }

        if (soul.isPullCurse && Mathf.Sqrt(Mathf.Abs(Mathf.Pow(soul.getVelocity().x, 2) - Mathf.Pow(soul.getVelocity().y, 2))) < 0.1)
        {
            transform.position = soul.transform.position;
            soul.isPullCurse = false;
            soul.isPullBack = false;
        }
    }

    void Rotate()
    {
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

    void OnCollisionEnter2D(Collision2D other)
    {


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

