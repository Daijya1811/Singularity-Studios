using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamerasController : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField]
    List<Camera> playerCameras = new List<Camera>();
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

    public void SetCamera(PlayerInput playerInput)
    {
        playerCameras.Add( playerInput.transform.parent.GetComponentInChildren<Camera>());
    }
}
