using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedObject : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Release() {
        anim.SetBool("IsPossess", false);
    }
}
