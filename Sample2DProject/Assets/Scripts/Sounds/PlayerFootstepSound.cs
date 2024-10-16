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
    }
    

    /// <summary>
    /// Plays a footstep if the animator is not in transition.
    /// </summary>
    public void PlayFootstep()
    {
        Debug.Log("hellp");
        if (/*!animator.IsInTransition(0) &&*/ footstepSounds.Length > 0)
        {
            // Pick a random footstep sound from the array
            int index = Random.Range(0, footstepSounds.Length);
            AudioClip footstepClip = footstepSounds[index];

            //set random pitch
            audioSource.pitch = Random.Range(pitchMin, pitchMax);
            // Play the sound
            audioSource.PlayOneShot(footstepClip, footStepVolume);
        }
    }
}
