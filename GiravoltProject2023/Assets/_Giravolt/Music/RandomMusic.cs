using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusic : MonoBehaviour
{
    public float minDelay = 5f;
    public float maxDelay = 10f;
    public AudioClip[] musicTracks;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("PlayRandomMusic", Random.Range(minDelay, maxDelay));
    }

    void PlayRandomMusic()
    {
        int trackIndex = Random.Range(0, musicTracks.Length);
        audioSource.clip = musicTracks[trackIndex];
        audioSource.Play();
        Invoke("PlayRandomMusic", Random.Range(minDelay, maxDelay));
    }
}