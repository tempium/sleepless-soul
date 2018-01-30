using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public PlayerSoul soul;
    public Firewall fire;
    public GameObject gameOver;
    public GameObject Gauge;
    float tmp;

	// Use this for initialization
	void Start () {
        tmp = Time.time;
        gameOver.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if(soul == null)
        {
            gameOver.SetActive(true);
            Gauge.SetActive(false);
            return;
        }
        if (soul != null)
        {
            this.GetComponent<Text>().text = "Score :" + ((int)(Time.time - tmp)*100);
        }
    }
}
