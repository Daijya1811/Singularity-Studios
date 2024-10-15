using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton script
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