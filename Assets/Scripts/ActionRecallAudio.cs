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

    [SerializeField] private Animator greenCrystalAnimator;
    [SerializeField] private Animator wrongPanelAnimator;
    private void Start()
    {
        // Get the reference to the AudioPlayer script attached to the GameObject
        audioPlayer = GameObject.FindObjectOfType<AudioPlayer>();
        playerInsideTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (AudioPlayer.audioSequenceIsPlaying != true)
        {
            playerInsideTrigger = true;
            crystalLights[audioSourceElement].SetActive(true);
        }
        Debug.Log("In Trigger " + audioSourceElement);
    }
    private void OnTriggerExit(Collider other)
    {
        playerInsideTrigger = false;
        crystalLights[audioSourceElement].SetActive(false);
    }
    private void Update()
    {
        // Checks everytime the player gets the whole sequence right
        if (actionCounter >= AudioPlayer.generatedAudioNumbers.Length)
        {
            actionCounter = 0;
            //When player gets it all right start a new sequence
            AudioPlayer.amountOfAudioPlayed++;
            //When player gets it all right start a new sequence
            StartCoroutine(PlayRandomAudioAfterDelay());
            if (AudioPlayer.amountOfAudioPlayed == 6)
            {
                // screen shake and animate a new crystal
                ScreenShake.startShake = true;
                greenCrystalAnimator.SetBool("Move", true);
                AudioPlayer.crystalAvailableInLevel = 3;
            }
        }
        if (playerInsideTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(actionCounter);
            // Checks if player has interacted with the wrong crystal
            if (AudioPlayer.generatedAudioNumbers[actionCounter] != audioSourceElement)
            {
                StartCoroutine(PlayPanelAnimationDelay());
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
    private IEnumerator PlayPanelAnimationDelay()
    {
        wrongPanelAnimator.SetBool("Play", true);

        yield return new WaitForSeconds(2.5f);
        wrongPanelAnimator.SetBool("Play", false);
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