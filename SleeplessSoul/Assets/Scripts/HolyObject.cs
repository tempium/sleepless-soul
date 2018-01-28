using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyObject : MonoBehaviour
{
    public float pushForce;

    private Rigidbody2D rb;
    private PointEffector2D pointEffector;

    // Use this for initialization
    void Start()
    {
        pointEffector = GetComponent<PointEffector2D>();
        pointEffector.forceMagnitude = pushForce;
    }

}