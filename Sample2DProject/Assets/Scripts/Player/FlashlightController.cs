using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [SerializeField] private bool flashlightEnabledOnStart = false;
    [SerializeField] private float rechargeScaler = 1;
    [SerializeField] private float batteryChargeDepletionScaler = 3;
    
    public float CurrentBatteryTime
    {
        get { return currentBatteryTime; }
    }

    public bool FlashlightEnabled
    {
        get { return flashlight ? flashlight.enabled : false; }
    }
    
    public float TotalBatteryTime
    {
        get { return startBatteryTime; }
    }
    
    // Private variables
    private Light flashlight;
    private AudioSource audioSource;
    public float currentBatteryTime= 100f;

    void Start()
    {
        flashlight = GetComponent<Light>();
        flashlight.enabled = flashlightEnabledOnStart;
        
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;

        currentBatteryTime = startBatteryTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (flashlight.enabled)
        {
            currentBatteryTime -= Time.deltaTime * batteryChargeDepletionScaler;
            if (currentBatteryTime <= 0)
            {
                currentBatteryTime = 0;
                flashlight.enabled = false;
                PlayAudioEffect(endClip);
            }
        }
        if(dimLightWithTime) flashlight.intensity =  Math.Min(currentBatteryTime, startBatteryTime) / startBatteryTime;

        if (!flashlight.enabled)
        {
            currentBatteryTime += Time.deltaTime / rechargeScaler;
            if (currentBatteryTime > startBatteryTime) currentBatteryTime = startBatteryTime;
        }
    }

    
    //enables or disables the flashlight based on outside input
    public void TriggerFlashLight(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

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
