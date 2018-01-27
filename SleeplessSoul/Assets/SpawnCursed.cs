using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCursed : MonoBehaviour {

	public Transform curseObj;
	private const float max_x = 2.4f;
	private const float max_y = 4f;
	// Use this for initialization
	void Start () {
		for (int i=0 ; i<5; i++){
			float tempX = Random.Range (-max_x, max_x);
			float tempY = Random.Range (1.5f, max_y);
			Instantiate (curseObj, new Vector3 (tempX, 
												tempY, 0), 
												curseObj.rotation);
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
