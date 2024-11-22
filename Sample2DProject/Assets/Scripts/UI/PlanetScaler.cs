using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScaler : MonoBehaviour
{
    [SerializeField] private float maxScale;
    [SerializeField] private float scaler;

    bool sizeLimitReached;

    public bool SizeLimitReached { get { return sizeLimitReached; } }
    void Update()
    {
        if (transform.localScale.x > maxScale) 
        { 
            sizeLimitReached = true; 
            // print(Time.timeSinceLevelLoad);  
            return; 
        }
        transform.localScale += Vector3.one * scaler * Time.deltaTime;
    }
}
