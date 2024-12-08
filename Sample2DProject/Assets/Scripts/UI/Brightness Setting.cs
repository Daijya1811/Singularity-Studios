using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class BrightnessSetting : MonoBehaviour
{
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private PostProcessProfile brightness;
    [SerializeField] private PostProcessLayer layer;

    AutoExposure exposure;

    void Start()
    {
        brightness.TryGetSettings(out exposure);
        AdjustBrighness(1f);
    }

    public void AdjustBrighness(float value)
    {
        if(value != 0)
        {
            exposure.keyValue.value = value;
        }
        else
        {
            exposure.keyValue.value = 0.05f;
        }
    }
}
