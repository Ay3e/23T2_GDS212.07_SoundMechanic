using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSource;

    [SerializeField] private GameObject blueCrystal;
    [SerializeField] private GameObject redCrystal;

    [SerializeField] private GameObject blueLight;
    [SerializeField] private GameObject redLight;

    private int amountOfAudioPlayed = 3;
    private int crystalAvailableInLevel = 2;
    private int randomAudioNumber;
    private AudioSource playRandomAudioSource;
    private int numberOfAudioPlayed;
    public static int[] generatedAudioNumbers; // New array to store generated randomAudioNumber values

    private void Start()
    {
        generatedAudioNumbers = new int[amountOfAudioPlayed]; // Initialize the array with the size of amountOfAudioPlayed
        PlayRandomAudio();
        blueCrystal.GetComponent<PlayerInputTracker>().enabled = false;
        redCrystal.GetComponent<PlayerInputTracker>().enabled = false;

        blueLight.SetActive(false);
        redLight.SetActive(false);
    }

    public void PlayRandomAudio()
    {
        StartCoroutine(PlayRandomAudioWithDelay());
    }

    private IEnumerator PlayRandomAudioWithDelay()
    {
        blueCrystal.GetComponent<PlayerInputTracker>().enabled = false;
        redCrystal.GetComponent<PlayerInputTracker>().enabled = false;
        for (int i = 0; i < amountOfAudioPlayed; i++)
        {
            numberOfAudioPlayed++;
            // Generate a random number between 0 (inclusive) and crystalAvailableInLevel (exclusive)
            randomAudioNumber = Random.Range(0, crystalAvailableInLevel);

            // Ensure the randomAudioNumber is within the bounds of the audioSource array
            randomAudioNumber = Mathf.Clamp(randomAudioNumber, 0, audioSource.Length - 1);

            // Store the randomAudioNumber in the array
            generatedAudioNumbers[i] = randomAudioNumber;

            // Get the AudioSource associated with the randomAudioNumber
            playRandomAudioSource = audioSource[randomAudioNumber];

            // Play the selected random audio
            if (playRandomAudioSource != null)
            {
                playRandomAudioSource.Play();
            }

            // Wait for 1.5 seconds before playing the next audio
            yield return new WaitForSeconds(1.5f);
        }
        // After all audio is played, you can access the generatedAudioNumbers array to see all the randomAudioNumber values generated.
        // For example, you can use Debug.Log to display them:
        Debug.Log("All Random Audio Numbers Generated: " + string.Join(", ", generatedAudioNumbers));
        // And set all GameManagers to be on
        blueCrystal.GetComponent<PlayerInputTracker>().enabled = true;
        redCrystal.GetComponent<PlayerInputTracker>().enabled = true;
    }
}