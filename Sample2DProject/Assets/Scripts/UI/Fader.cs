using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// This script fades the screen to white. 
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class Fader : MonoBehaviour
{
    [SerializeField] float fadeDuration = 2f;
    CanvasGroup canvasGroup;

    public float FadeDuration { get { return fadeDuration; } }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public IEnumerator FadeAndReset()
    {
        yield return StartCoroutine(FadeRoutine(1f, fadeDuration));
        yield return new WaitForSeconds(0.33f);
        yield return StartCoroutine(FadeRoutine(0f, fadeDuration));
    }
    public IEnumerator FadeRoutine(float target, float time)
    {
        while (!Mathf.Approximately(canvasGroup.alpha, target))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, target, (((Time.deltaTime * Time.deltaTime) / time) * (3.0f - 2.0f * (Time.deltaTime / time))) / time);
            yield return null;
        }
    }
}