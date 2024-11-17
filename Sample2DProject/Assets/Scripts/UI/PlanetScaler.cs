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
        if (transform.localScale.x > maxScale) { sizeLimitReached = true; return; }
        transform.localScale =  new Vector3(transform.localScale.x * scaler, transform.localScale.y * scaler, transform.localScale.z);
    }
}
