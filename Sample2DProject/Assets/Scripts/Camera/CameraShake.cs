using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraShake : MonoBehaviour
{
    public Transform camTransform;
    
    
    public float shakeAmount = 0.7f;

    Vector3 originalPos;

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
    }
}

