using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScaler : MonoBehaviour
{
    [SerializeField] private float maxScale;
    [SerializeField] private float scaler; 
    
    
    
    void Update()
    {   
        if(transform.localScale.x < maxScale) transform.localScale =  new Vector3(transform.localScale.x * scaler, transform.localScale.y * scaler, transform.localScale.z);
    }
}
