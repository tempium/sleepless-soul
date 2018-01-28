using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Dialog2 : MonoBehaviour {

    private int state;
    int count;
    public Dialog1 picture1;
    // Use this for initialization
    void Start()
    {
        state = 0;
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(picture1.state != 3)
        {
            return;
        }
        if (state == 0)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.07f);
            print(transform.position.y);
            count++;
            if (count == 135)
            {
                count = 0;
                state = 1;
            }
        }
        else if (state == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                state = 2;

            }
        }
        else if (state == 2)
        {
            SceneManager.LoadScene(0);

        }
    }
}
