using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRecallAudio : MonoBehaviour
{
    [SerializeField] private GameObject[] crystalLights;
    [SerializeField] private int audioSourceElement;
    public static int actionCounter = 0;
    public AudioPlayer audioPlayer;
    private bool playerInsideTrigger;
    private bool isPlayingRandomAudio = false;

    private void Start()
    {
        // Get the reference to the AudioPlayer script attached to the GameObject
        audioPlayer = GameObject.FindObjectOfType<AudioPlayer>();
        playerInsideTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        playerInsideTrigger = true;
        crystalLights[audioSourceElement].SetActive(true);
        Debug.Log("In Trigger " + audioSourceElement);
       
    }
    private void OnTriggerExit(Collider other)
    {
        playerInsideTrigger = false;
        crystalLights[audioSourceElement].SetActive(false);
    }
    private void Update()
    {
        if (playerInsideTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if(actionCounter >= AudioPlayer.generatedAudioNumbers.Length)
            {
                actionCounter = 0;
            }
            Debug.Log(actionCounter);
            if (AudioPlayer.generatedAudioNumbers[actionCounter] != audioSourceElement)
            {
                // Check if we are already playing random audio to avoid overlapping calls
                if (!isPlayingRandomAudio)
                {
                    // Start the coroutine to play random audio after a delay
                    StartCoroutine(PlayRandomAudioAfterDelay());
                    // Reset Counter
                    actionCounter = 0;
                }
            }
            else
            {
                // If the generatedAudioNumber is equal to audioSourceElement, you can perform some other action or logic here if needed.
                actionCounter++;
            }
        }
    }
    private IEnumerator PlayRandomAudioAfterDelay()
    {
        isPlayingRandomAudio = true;
        //disable audio input and light
        for(int i =0; i < crystalLights.Length; i++)
        {
            crystalLights[i].SetActive(false);
        }
        gameObject.GetComponent<PlayerInputTracker>().enabled = false;
        // Wait for 3 seconds 
        yield return new WaitForSeconds(3f);

        // Play the random audio
        audioPlayer.PlayRandomAudio();
        

        // Reset the flag
        isPlayingRandomAudio = false;
    }
}