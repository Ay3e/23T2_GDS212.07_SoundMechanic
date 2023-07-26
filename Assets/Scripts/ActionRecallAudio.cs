using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRecallAudio : MonoBehaviour
{
    [SerializeField] private int audioSourceElement;
    public static int actionCounter = 0;
    private AudioPlayer audioPlayer;
    private bool playerInsideTrigger;

    private void Start()
    {
        // Get the reference to the AudioPlayer script attached to the GameObject
        audioPlayer = GameObject.FindObjectOfType<AudioPlayer>();
        playerInsideTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        playerInsideTrigger = true;
        Debug.Log("In Trigger " + audioSourceElement);
       
    }
    private void OnTriggerExit(Collider other)
    {
        playerInsideTrigger = false;
    }
    private void Update()
    {
        if (playerInsideTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(actionCounter);
            if (actionCounter >= audioSourceElement)
            {
                actionCounter = 0;
            }
            if (AudioPlayer.generatedAudioNumbers[actionCounter] != audioSourceElement)
            {
                audioPlayer.PlayRandomAudio();
                actionCounter = 0;
            }
            else
            {
            // If the generatedAudioNumber is equal to audioSourceElement, you can perform some other action or logic here if needed.
            actionCounter++;
            }
        }
    }
}