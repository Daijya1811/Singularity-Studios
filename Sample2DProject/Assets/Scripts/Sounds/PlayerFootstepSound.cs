using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class PlayerFootstepSound : MonoBehaviour
{
    [Header("Audio Clips and Pitch Range, along with the animator")]
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private Animator animator;
    [SerializeField] private float footStepVolume = .01f;

    [Range(-3,3)]
    [SerializeField] private float pitchMin;
    [Range(-3,3)]
    [SerializeField] private float pitchMax;
    
    

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        SetRandomFootstepSound();
    }
    

    /// <summary>
    /// Plays a footstep if the animator is not in transition.
    /// </summary>
    public void PlayFootstep()
    {
        if (footstepSounds.Length <= 0) return;
        if (audioSource.isPlaying) return;
       
        //set random pitch
        audioSource.pitch = Random.Range(pitchMin, pitchMax);
        // Play the sound
        audioSource.Play();
    }

    private void SetRandomFootstepSound()
    {
        int index = Random.Range(0, footstepSounds.Length);
        AudioClip footstepClip = footstepSounds[index];
        audioSource.clip = footstepClip;
    }
}
