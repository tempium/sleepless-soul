using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public PlayerSoul soul;
    float tmp;

	// Use this for initialization
	void Start () {
        tmp = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (soul != null)
        {
            this.GetComponent<Text>().text = "Score :" + ((int)(Time.time - tmp)*100);
        }
    }
}
