using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;

    private const string MIXER_MASTER = "MasterVolume";
    void Awake()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
    }

    void SetMasterVolume(float value)
    {
        mixer.SetFloat(MIXER_MASTER, Mathf.Log10(value) * 20); 
    }
}
