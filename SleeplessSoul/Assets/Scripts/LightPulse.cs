using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPulse : MonoBehaviour {

	private Light myLight;
	private int pulseTime;

	// Use this for initialization
	void Start () {
		myLight = GetComponent<Light> ();
		pulseTime = Random.Range(0, 10);
	}
	
	// Update is called once per frame
	void Update () {
		float magic = (Time.time * 20) % 20 - pulseTime;
		int min = (magic < 10 && magic > 0) ? -5 : -1;
		int max = (magic < 10 && magic > 0) ? 2 : 6;
		int random = Random.Range(min, max);
		myLight.intensity += Mathf.Clamp(random, -1, 1) * 10 * Time.deltaTime;
		myLight.intensity = Mathf.Clamp (myLight.intensity, 3, 8);
	}
}
