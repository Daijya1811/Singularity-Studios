using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioClip(AudioClip audioClip, float volume)
    {
        audioSource.PlayOneShot(audioClip, volume);
    }
    public IEnumerator PlayAudioClipAtRandom(AudioClip audioClip, float volume, float min, float max)
    {
        float randomSeconds = UnityEngine.Random.Range(min, max);
        yield return new WaitForSeconds(randomSeconds);
        PlayAudioClip(audioClip, volume);
    }
}
