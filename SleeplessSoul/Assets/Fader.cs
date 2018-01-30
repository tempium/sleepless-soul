using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Fader : MonoBehaviour {
    public bool fadeIn;
    public bool fadeOut;
    public int load;
    private SpriteRenderer sr;
    private float startTime;


    // Use this for initialization
    void Start () {
        fadeIn = true;
        fadeOut = false;
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1,1,1,1);
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update () {
        if (fadeIn)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - 1*Time.deltaTime);
            if(sr.color.a <= 0)
            fadeIn = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Time.time - startTime > 2)
        {
            fadeIn = false;
            fadeOut = true;
        }
        if (fadeOut)
        {

            sr.color = new Color(1, 1, 1, sr.color.a + 1 * Time.deltaTime);
            if (sr.color.a >= 1) SceneManager.LoadScene(load);

        }
    }

    
}
