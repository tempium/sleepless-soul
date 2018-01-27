using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMovement : MonoBehaviour {

    public float backgroundLength = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y <= -backgroundLength) {
            transform.position = new Vector3(0, 0, transform.position.z);
        }
	}
}
