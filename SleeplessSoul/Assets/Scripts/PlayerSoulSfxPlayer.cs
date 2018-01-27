using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoulSfxPlayer : MonoBehaviour {

    public AudioSource DepartSfx;
    public AudioSource SuckbackSfx;
    public AudioSource PossessSfx;

    public void PlayDepartSfx()
    {
        DepartSfx.Play();
    }

    public void PlaySuckbackSfx()
    {
        SuckbackSfx.Play();
    }

    public void PlayPossessSfx()
    {
        PossessSfx.Play();
    }

    public void StopAllSfx()
    {
        if (DepartSfx.isPlaying)
        {
            DepartSfx.Stop();
        }
        if (SuckbackSfx.isPlaying)
        {
            SuckbackSfx.Stop();
        }
        if (PossessSfx.isPlaying)
        {
            PossessSfx.Stop();
        }
    }
}
