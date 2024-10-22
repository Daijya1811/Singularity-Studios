using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class PlayerFootstepSound : MonoBehaviour
{
    [Header("Audio Clips and Pitch Range, along with the animator")]
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private float footStepVolume = .01f;

    [Range(-3,3)]
    [SerializeField] private float pitchMin;
    [Range(-3,3)]
    [SerializeField] private float pitchMax;

    [Header("Animator Thresholds")] 
    [SerializeField] private float walkThreshold = 0.5f;
    [SerializeField] private float slowRunThreshold = 1.25f;
    [SerializeField] private float sprintThreshold = 2.0f;
   
    [Header("Additional Needed Components")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Animator animator;

    

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }
    

    /// <summary>
    /// Plays a footstep if the weight matches
    /// Takes in a target speed of the threshold of the animation. This is to prevent double playing.
    /// </summary>
    public void PlayFootstep(float speed)
    {
        if (footstepSounds.Length <= 0) return;

        //if the movement speed of the animator does not match the speed of the current animation event, do not play
        //not sure how efficient but better than havuing double playing sounds
        if(GetMovementStateBasedOnThresholds(animator.GetFloat("speed")) == GetMovementStateBasedOnThresholds(speed))
        {
            //set random pitch
            audioSource.pitch = Random.Range(pitchMin, pitchMax);
        
            // Play the sound
            audioSource.PlayOneShot(GetRandomFootstepSound(), footStepVolume);   
        }
    }

    /// <summary>
    /// Helper function that grabs a random audio clip.
    /// </summary>
    /// <returns></returns>
    private AudioClip GetRandomFootstepSound()
    {
        int index = Random.Range(0, footstepSounds.Length);
        AudioClip footstepClip = footstepSounds[index];
        return footstepClip;
    }

    //gets the speed threshold based on a given speed
    private int GetMovementStateBasedOnThresholds(float speed)
    {
        if (speed <= walkThreshold) return 0;

        if (speed <= slowRunThreshold) return 1;

        if (speed <= sprintThreshold) return 2;

        return -1;
    }
}
