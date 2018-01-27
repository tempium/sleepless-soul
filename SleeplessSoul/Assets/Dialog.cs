using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Dialog : MonoBehaviour {
    // Use this for initialization
    private int state;
	void Start () {
        state = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if(state == 1)
        {
            this.GetComponent<Text>().text = "Test State 1";
        }else if(state == 2)
        {
            this.GetComponent<Text>().text = "Test State 2";
        }
        else
        {
            SceneManager.LoadScene(1);

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            state++;
        }
    }
}
