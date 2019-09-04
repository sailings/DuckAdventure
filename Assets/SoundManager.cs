using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    public static SoundManager Instance;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Instance = this;
    }

    public void PlaySound(AudioClip audioClip)
    {
        if (audioClip)
            audioSource.PlayOneShot(audioClip);
    }
}
