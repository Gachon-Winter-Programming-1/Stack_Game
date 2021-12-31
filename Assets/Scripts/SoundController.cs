using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
public class SoundController : Singleton<SoundController>
{
    public AudioSource audioSource;
    public AudioClip missCut;
    public AudioClip[] pianoKey;
    private int keyIndex = 0;
    public void UpKeyPlay()
    {
        audioSource.clip = pianoKey[keyIndex++ % pianoKey.Length];
        audioSource.Play();
    }

    public void ResetKeyPlay()
    {
        keyIndex = 0;
        audioSource.clip = missCut;
        audioSource.Play();
    }
    
}
