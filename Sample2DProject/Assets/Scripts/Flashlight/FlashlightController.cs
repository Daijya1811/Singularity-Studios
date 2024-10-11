using System;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class FlashlightController : MonoBehaviour
{
    [Header("Sounds")] 
    [SerializeField] private AudioClip startClip;
    [SerializeField] private AudioClip endClip;
    [SerializeField] private AudioClip chargeClip;
    
    [Header("Battery Settings")]
    [SerializeField] private float startBatteryTime= 100f;
    [SerializeField] private bool dimLightWithTime = true;
    
    // Private variables
    private Light flashlight;
    private AudioSource audioSource;
    public float currentBatteryTime= 100f;

    void Start()
    {
        flashlight = GetComponent<Light>();
        flashlight.enabled = false;
        
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;

        currentBatteryTime = startBatteryTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(flashlight.enabled) currentBatteryTime -= Time.deltaTime;
        if(dimLightWithTime) flashlight.intensity = (currentBatteryTime) / startBatteryTime;
    }

    
    //enables or disables the flashlight based on outside input
    public void TriggerFlashLight()
    {
        flashlight.enabled = !flashlight.enabled;

        //if enabled turn off
        if (flashlight.enabled)
        {
            PlayAudioEffect(startClip);
            return;
        }
        PlayAudioEffect(endClip);
    }

    public void ChargeFlashlight()
    {
        currentBatteryTime = startBatteryTime;
        PlayAudioEffect(chargeClip);
    }

    /// <summary>
    /// Plays a clip from the AudioSource
    /// </summary>
    /// <param name="clip"></param>
    private void PlayAudioEffect(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
