using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamerasController : MonoBehaviour
{
    public List<Camera> playerCameras = new List<Camera>();
    Camera mainCamera;
    void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
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

    public void SetCamera(PlayerInput playerInput)
    {
        playerCameras.Add(playerInput.transform.GetComponentInChildren<Camera>());
    }
}
