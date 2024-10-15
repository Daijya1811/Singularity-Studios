using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script makes the GameObject it is attached to persist through scenes. 
/// In essence, this is just a Singleton class. 
/// </summary>
public class KeepAlive : MonoBehaviour
{
    private GameObject sceneManager;

    private void Awake()
    {
        if (sceneManager == null)
        {
            sceneManager = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}