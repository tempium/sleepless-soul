using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicWrapper : MonoBehaviour {

    public float loopLength;
    public float loopStartPosition;

    private AudioSource audioSource;
    private AudioClip audioClip;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioClip = audioSource.clip;
    }


    
    public void Update()
    {
        if (loopLength > 0 && loopStartPosition > 0)
        {
            if (audioSource.timeSamples > loopStartPosition * audioClip.frequency)
            {
                audioSource.timeSamples -= Mathf.RoundToInt(loopLength * audioClip.frequency);
            }
        }
        
    }
}
