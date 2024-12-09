using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OscillateSkipCutsceneText : MonoBehaviour
{
    [SerializeField] float fadeSpeed = 1f;
    TMP_Text skipCutsceneText;
    void Start()
    {
        skipCutsceneText = GetComponent<TMP_Text>();
    }
    void Update()
    {
        OscillateOpacity();
    }
    void OscillateOpacity()
    {
        float textAlpha = 0.5f * (Mathf.Sin((Time.time * fadeSpeed) - (Mathf.PI / 2)) + 1); //Normalizes oscillations between 0 and 1 instead of -1 and 1
        skipCutsceneText.alpha = textAlpha;
    }
}
