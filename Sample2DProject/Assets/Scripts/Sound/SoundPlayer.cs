using UnityEngine;
/// <summary>
/// This class is intended to work with additional scripts to play sounds from an object.
/// Made it as animation events can pass through a sound file in their event and it seemed redundant to do this per object.
/// </summary>
public class SoundPlayer : MonoBehaviour
{

    [Header("Audio Manager and Needed Clips")] 
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    //plays a sound
    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    //plays a sound OneShot
    public void PlaySoundOneShot(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    
    //plays a sound OneShot with volume
    public void PlaySoundOneShot(AudioClip clip, float volume)
    {
        audioSource.PlayOneShot(clip, volume);
    }
}
