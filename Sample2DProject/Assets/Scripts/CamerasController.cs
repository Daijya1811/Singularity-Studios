using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamerasController : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] Camera[] playerCameras;
    void Awake()
    {
        mainCamera = Camera.main;
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
