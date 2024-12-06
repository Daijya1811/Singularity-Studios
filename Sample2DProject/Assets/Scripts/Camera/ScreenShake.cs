using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will shake the main camera around. 
/// </summary>
public class ScreenShake : MonoBehaviour
{
    [Header("Shake Modifiers")]
    [SerializeField] float shakeDuration = 3f;
    float shakeIntensity = 1f;

    [Header("RNG Frequency Values")]
    [Tooltip("Sets the minimum amount of time to wait in seconds until the next shake when put through a Random function.")]
    [SerializeField] float rngMin;
    [Tooltip("Sets the maximum amount of time to wait in seconds until the next shake when put through a Random function.")]
    [SerializeField] float rngMax;

    float timerForWaiting = 0f;
    float timerForShaking = 0f;
    float timeUntilNextShake;

    AudioSource audioSource;
    bool isPlayingAudio;

    [SerializeField] bool isMainMenu = false;
    [Header("Reference to Cinematic To Prevent Shaking During Cutscene")]
    [SerializeField] CinematicControlRemover introCinematic;

    PlanetScaler planet;

    public float ShakeIntensity { get { return shakeIntensity; } }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        planet = FindObjectOfType<PlanetScaler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        timeUntilNextShake = Random.Range(rngMin, rngMax);
    }
    private void Update()
    {
        if (introCinematic != null && !introCinematic.DonePlaying) return;
        print("amogus");
        RunTimers();

        // isMainMenu should be false during gameplay scenes.
        if (isMainMenu) ShakeScreen(this.gameObject, this.shakeIntensity);
    }
    /// <summary>
    /// Shakes the "screen." Really, it shakes the position of the midCameraPoint instead (as the camera is always looking at it). 
    /// </summary>
    /// <param name="midCameraPrefab"> midCameraPrefab GameObject instance. </param>
    public void ShakeScreen(GameObject midCameraPrefab, float shakeIntensity)
    {
        if (isMainMenu) shakeIntensity = 0.0125f;
        
        if (CanShake() && timerForShaking < shakeDuration)
        {
            midCameraPrefab.transform.position += (Vector3) Random.insideUnitCircle * shakeIntensity;
            if (!isPlayingAudio)
            {
                isPlayingAudio = true;
                audioSource.Play();
            }
        }
        else if(timerForShaking >= shakeDuration)
        {
            timerForWaiting = 0f;
            timerForShaking = 0f;
            timeUntilNextShake = Random.Range(rngMin, rngMax);
            isPlayingAudio = false;

            if (isMainMenu) midCameraPrefab.transform.position = Vector3.zero;
        }

        if(!isMainMenu)
        {
            if(planet.transform.localScale.x < 10f) shakeIntensity = 1f;
            else if (planet.transform.localScale.x < 25f) shakeIntensity = 1.25f;
            else if (planet.transform.localScale.x < 50f) shakeIntensity = 1.5f;
            else if (planet.transform.localScale.x < 100f) shakeIntensity = 2f;
            else if (planet.transform.localScale.x < 200f) shakeIntensity = 2.5f;
            else shakeIntensity = 4f;
        }
    }
    /// <summary>
    /// Determines if the screen is allowed to shake or not depending on the time since last shake. 
    /// </summary>
    /// <returns> true if can shake, false if cannot shake yet. </returns>
    public bool CanShake()
    {
        if (timerForWaiting < timeUntilNextShake) return false;

        // else return true
        return true;
    }
    void RunTimers()
    {
        timerForWaiting += Time.deltaTime;
        if(CanShake())
        {
            timerForShaking += Time.deltaTime;
        }
    }
}
