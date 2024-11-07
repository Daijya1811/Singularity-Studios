using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] sfx;

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
