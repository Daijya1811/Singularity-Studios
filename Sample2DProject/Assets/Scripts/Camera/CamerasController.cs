using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class stores all split-screen player cameras in a reference, and allows toggling between the main camera with the split-screen camera. 
/// </summary>
public class CamerasController : MonoBehaviour
{
    List<Camera> playerCameras = new List<Camera>();
    Camera mainCamera;
    void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        // Find and store the split-screen cameras. 
        GameObject[] playerCams = GameObject.FindGameObjectsWithTag("PlayerCamera");
        playerCameras.Add(playerCams[0].GetComponent<Camera>());
        playerCameras.Add(playerCams[1].GetComponent<Camera>());
    }

    /// <summary>
    /// Toggles between Split-Screen cameras and Main Camera.
    /// </summary>
    public void ToggleCamera()
    {
        mainCamera.enabled = !mainCamera.enabled;
        foreach(Camera camera in playerCameras)
        {
            camera.enabled = !camera.enabled;
        }
    }
}
