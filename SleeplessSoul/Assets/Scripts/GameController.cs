using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController instance;
	public GameObject gameOverText;
	public bool gameOver = false;
	public float scrollSpeed = 1;

    public GameObject[] objectFrames;

    private float screenHeight;
    private float initialOffset;

    private Queue<GameObject> frameBuffer;
    private int frameBufferSize = 3;
    
	// Use this for initialization
	void Start () {

        // Instanitiate Queue
        frameBuffer = new Queue<GameObject>(frameBufferSize);

        // Reference from Unity Questions.
        screenHeight = Camera.main.orthographicSize * 2;
        initialOffset = 0.5f * screenHeight;

        // The first 2 frames
        for (int i = 0; i < frameBufferSize; i++)
        {
            frameBuffer.Enqueue(Instantiate(getRandomFrame(), new Vector2(0, initialOffset + screenHeight * i), Quaternion.identity));
        }

        Debug.Log(Camera.main.orthographicSize * 2);
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);
        Debug.Log(Screen.currentResolution);
	}
	
	// Update is called once per frame
	void Update () {
        GameObject lowestFrame = frameBuffer.Peek();

        Debug.Log(lowestFrame.transform.position);
        // Check if the lowestFrame is below the screen
        if (lowestFrame.transform.position.y <= -screenHeight)
        {
            // Destroy the lowestFrame
            lowestFrame = frameBuffer.Dequeue();
            Destroy(lowestFrame);
            frameBuffer.Enqueue(Instantiate(getRandomFrame(), new Vector2(0, screenHeight * (frameBufferSize - 1)), Quaternion.identity));
        }
	}

	private GameObject getRandomFrame()
    {
        int index = Random.Range(0, objectFrames.Length);
        return objectFrames[index];
    }
}
