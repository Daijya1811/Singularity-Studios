using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script makes the GameObject it is attached to persist through scenes. 
/// In essence, this is just a Singleton class. 
/// </summary>
public class KeepAliveSingleton : MonoBehaviour
{
    private static GameObject instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = gameObject;
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
}