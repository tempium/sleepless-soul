using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollingObject : MonoBehaviour {

	private Rigidbody2D RIGIDBODY;
	private float speed = -2f;
	[SerializeField]private bool stop_scrolling;

	// Use this for initialization
	void Start () {
		RIGIDBODY = GetComponent<Rigidbody2D> ();
		RIGIDBODY.velocity = new Vector2(0, speed);
	}


	// Update is called once per frame
	void Update () {
		if (stop_scrolling) {
			RIGIDBODY.velocity = Vector2.zero;
		} else {
			RIGIDBODY.velocity = new Vector2 (0, speed);
		}
	}
}
