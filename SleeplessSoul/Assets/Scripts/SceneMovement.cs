using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMovement : MonoBehaviour {

    public float scrollSpeed = 1;

    private GameObject player;
    private bool boost = false;
    //private float scrollSpeed;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        //scrollSpeed = GameController.instance.scrollSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        if (player == null)
        {

            return;
        }
        if (player.transform.position.y > 2) {
            boost = true;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y - (player.transform.position.y) * scrollSpeed * Time.deltaTime, transform.position.z);
        //transform.position = new Vector3(transform.position.x, transform.position.y - stateSpeed * Time.deltaTime, transform.position.z);
        if (boost) {
            transform.position = new Vector3(transform.position.x, transform.position.y - (player.transform.position.y) * scrollSpeed * Time.deltaTime, transform.position.z);
            if (player.transform.position.y <= 0) {
                boost = false;
            }
        }
	}

}
