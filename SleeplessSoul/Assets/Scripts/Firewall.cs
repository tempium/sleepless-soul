using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Firewall : MonoBehaviour {

    public float speed = 1;

    public GameObject firewallObject;

	private PlayerSoul soul;

    //[Range (0, 3.6f)]
    //public float collisionOffsetFromTop;

	// Use this for initialization
	void Start () {
		soul = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerSoul> ();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (soul != null)
            if (soul.transform.position.y - transform.position.y <= 0.2)
            {
                Destroy(soul);
            }
        if (Input.GetKeyDown(KeyCode.Space) && soul == null)
        {
            SceneManager.LoadScene(5);
        }
        if (soul == null)
            return;
        transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
        if (soul.transform.position.y - transform.position.y > 30) {
            transform.position = new Vector2(transform.position.x, soul.transform.position.y - 10);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
		//Debug.Log (other.gameObject.name);
        if (other.CompareTag("Player")) {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Cursed")) { 
			other.transform.parent.GetComponentInChildren<SpriteRenderer>().enabled = false;
			if (soul.linkedCursedObject.Equals(other.transform.parent.gameObject)) {
				Destroy (soul.gameObject);
			}
        }
        else if (other.CompareTag("Holy")) { 
			Destroy (other.gameObject);
        }
    }
    
}
