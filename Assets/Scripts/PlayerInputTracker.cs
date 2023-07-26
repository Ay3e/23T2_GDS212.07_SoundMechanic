using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputTracker : MonoBehaviour
{
    public AudioClip audioClip; // Assign the audio clip in the Unity Editor

    private bool playerInsideTrigger;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (playerInsideTrigger && Input.GetKeyDown(KeyCode.E))
        {
            PlayAudio();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        playerInsideTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerInsideTrigger = false;
    }

    private void PlayAudio()
    {
        audioSource.PlayOneShot(audioClip);
    }
}