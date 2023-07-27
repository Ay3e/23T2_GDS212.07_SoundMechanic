using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSource;
    [SerializeField] private GameObject[] crystalLights;

    [SerializeField] private GameObject blueCrystal;
    [SerializeField] private GameObject redCrystal;
    [SerializeField] private GameObject greenCrystal;

    //everytime 
    public static int amountOfAudioPlayed = 3;
    public static int crystalAvailableInLevel = 2;
    private int randomAudioNumber;
    private AudioSource playRandomAudioSource;
    public static int[] generatedAudioNumbers; // New array to store generated randomAudioNumber values

    public static bool audioSequenceIsPlaying = false;

    private void Start()
    {
        generatedAudioNumbers = new int[amountOfAudioPlayed]; // Initialize the array with the size of amountOfAudioPlayed
        PlayRandomAudio();
        blueCrystal.GetComponent<PlayerInputTracker>().enabled = false;
        redCrystal.GetComponent<PlayerInputTracker>().enabled = false;
        greenCrystal.GetComponent<PlayerInputTracker>().enabled = false;
    }

    public void PlayRandomAudio()
    {
        StartCoroutine(PlayRandomAudioWithDelay());
    }

    private IEnumerator PlayRandomAudioWithDelay()
    {
        generatedAudioNumbers = new int[amountOfAudioPlayed];
        audioSequenceIsPlaying = true;
        for (int i = 0; i < crystalLights.Length; i++)
        {
            crystalLights[i].SetActive(false);
        }
        for (int i = 0; i < amountOfAudioPlayed; i++)
        {
            randomAudioNumber = Random.Range(0, crystalAvailableInLevel);
            //Debug.Log(randomAudioNumber);
            generatedAudioNumbers[i] = randomAudioNumber;
            playRandomAudioSource = audioSource[randomAudioNumber];

            crystalLights[randomAudioNumber].SetActive(true);

            // Play the selected random audio
            if (playRandomAudioSource != null)
            {
                playRandomAudioSource.Play();
            }

            // Wait for 1.5 seconds before playing the next audio
            yield return new WaitForSeconds(1.5f);
            crystalLights[randomAudioNumber].SetActive(false);
        }
        // After all audio is played, you can access the generatedAudioNumbers array to see all the randomAudioNumber values generated.
        // For example, you can use Debug.Log to display them:
        audioSequenceIsPlaying = false;
        Debug.Log("All Random Audio Numbers Generated: " + string.Join(", ", generatedAudioNumbers));
        // And set all GameManagers to be on
        blueCrystal.GetComponent<PlayerInputTracker>().enabled = true;
        redCrystal.GetComponent<PlayerInputTracker>().enabled = true;
        greenCrystal.GetComponent<PlayerInputTracker>().enabled = true;
    }
}