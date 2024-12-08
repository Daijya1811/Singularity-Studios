using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class BrightnessSetting : MonoBehaviour
{
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private PostProcessProfile ppf;
    //[SerializeField] private PostProcessLayer layer;

    float gamma = 0f;
    ColorGrading colorGrading;

    void Start()
    {
        colorGrading = ppf.GetSetting<ColorGrading>();
        colorGrading.gamma.value = new Vector4(1f, 1f, 1f, gamma);

        //AdjustBrighness(1f);
    }

    private void Update()
    {
        gamma = brightnessSlider.value;
        colorGrading.gamma.value = new Vector4(1f, 1f, 1f, gamma);
    }

    /* public void AdjustBrighness(float value)
     {
         if(value != 0)
         {
             exposure.keyValue.value = value;
         }
         else
         {
             exposure.keyValue.value = 0.05f;
         }
     }*/
}
