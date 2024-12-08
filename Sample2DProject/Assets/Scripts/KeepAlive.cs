using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script makes the GameObject it is attached to persist through scenes. 
/// In essence, this is just a Singleton class. 
/// </summary>
public class KeepAlive : MonoBehaviour
{
    private static GameObject audioInstance;
    private static GameObject coOpManagerInstance;

    private void Awake()
    {
        if (audioInstance == null && this.gameObject.tag == "Audio")
        {
            audioInstance = gameObject;
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed across scenes
        }
        else if(coOpManagerInstance == null && this.gameObject.tag == "Co Op")
        {
            coOpManagerInstance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
}