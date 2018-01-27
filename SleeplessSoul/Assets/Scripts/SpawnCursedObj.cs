using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCursedObj : MonoBehaviour {

	public Transform curseObj;
	private const float max_x = 2.4f;
	private const float max_y = 4f;
	private const float offset_x = 0.3f;
	public int split_part = 2;
	// Use this for initialization
	void Start () {
		RandomRespawn();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void RandomRespawn(){
		float tempx = -max_x;
		float temp1 = (2 * max_x / split_part);
		float temp2 = tempx + temp1;
		for (int i = 0; i < split_part; i++) {
			float tempX = Random.Range (tempx + offset_x, temp2 - offset_x);
			float tempY = Random.Range (1.3f, max_y);
			print ("X: " + tempX);
			print ("Y: " + tempY);
			Instantiate (curseObj, new Vector3 (tempX, 
				tempY, 0), 
				curseObj.rotation);

			tempx += temp1;
			temp2 = tempx + temp1;
		}
	}

}
