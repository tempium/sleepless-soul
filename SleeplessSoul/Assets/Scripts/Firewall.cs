using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Firewall : MonoBehaviour {

    public float speed = 1;

    public GameObject firewallObject;
    public PlayerSoul soul;

    //[Range (0, 3.6f)]
    //public float collisionOffsetFromTop;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(soul != null)
        if(soul.transform.position.y - transform.position.y <= 0.2)
        {
                Destroy(soul);
        }
        if (Input.GetKeyDown(KeyCode.Space) && soul == null)
        {
            SceneManager.LoadScene(0);
        }
        if (soul == null)
            return;
        transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
		Debug.Log (other.gameObject.name);
        Destroy(other.gameObject);
    }
    
}
