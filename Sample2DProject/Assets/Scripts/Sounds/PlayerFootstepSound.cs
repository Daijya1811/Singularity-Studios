using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerFootstepSound : MonoBehaviour
{
    [Header("Audio Clips and Volume")]
    [SerializeField] private AudioClip[] footstepSounds; 
    [SerializeField] private float footstepVolume = 0.5f; 
    

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }
    

    public void PlayFootstep()
    {
        if (footstepSounds.Length > 0)
        {
            // Pick a random footstep sound from the array
            int index = Random.Range(0, footstepSounds.Length);
            AudioClip footstepClip = footstepSounds[index];

            // Play the sound
            audioSource.PlayOneShot(footstepClip, footstepVolume);
        }
    }
}
