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
    float camHeight;
    float camWidth;

    Vector3 p1Pos;
    Vector3 p2Pos;
    bool isMain = true;
    bool isSplit = false;
    FollowCamera followCamera;
    float midPointX;
    float midPointZ;

    [SerializeField] private Camera uiCamera;
    void Awake()
    {
        mainCamera = Camera.main;
        followCamera = GetComponent<FollowCamera>();
    }

    private void Start()
    {
        // Find and store the split-screen cameras. 
        GameObject[] playerCams = GameObject.FindGameObjectsWithTag("PlayerCamera");
        playerCameras.Add(playerCams[0].GetComponent<Camera>());
        playerCameras.Add(playerCams[1].GetComponent<Camera>());
        playerCameras.Add(playerCams[2].GetComponent<Camera>());
        playerCameras.Add(playerCams[3].GetComponent<Camera>());
        camHeight = 2f * mainCamera.orthographicSize;
        camWidth = camHeight * mainCamera.aspect;
    }
    private void Update()
    {
        // If there is only one player in the scene, then don't do anything. Return. 
        if (followCamera.Players[1] == null) return;

        p1Pos = followCamera.Players[0].transform.position;
        p2Pos = followCamera.Players[1].transform.position;
        midPointX = (p2Pos.x + p1Pos.x) / 2f;
        midPointZ = (p2Pos.z + p1Pos.z) / 2f;

        // if cam is not main yet both players are in bound, then transition to main cam
        if (!isMain && CheckForBounds(p1Pos) && CheckForBounds(p2Pos))
        {
            ToggleCamera();
        }
        // if cam is not split yet either player is out of bound, then transition to split cam
        else if((!isSplit && !CheckForBounds(p1Pos)) || (!isSplit && !CheckForBounds(p2Pos)))
        {
            ToggleCamera();
        }
    }

    /// <summary>
    /// Toggles between Split-Screen cameras and Main Camera.
    /// </summary>
    public void ToggleCamera()
    {
        mainCamera.enabled = !mainCamera.enabled;
        uiCamera.enabled = !uiCamera.enabled;
        isMain = !isMain;
        isSplit = !isSplit;
        foreach(Camera camera in playerCameras)
        {
            camera.enabled = !camera.enabled;
        }
    }

    private bool CheckForBounds(Vector3 PlayerPos)
    {
        if (PlayerPos.x > midPointX + camWidth / 2f)
        {
            return false;
        }
        if (PlayerPos.x < midPointX - camWidth / 2f)
        {
            return false;
        }
        if (PlayerPos.z > midPointZ + camHeight / 2f)
        {
            return false;
        }
        if (PlayerPos.z < midPointZ - camHeight / 2f)
        {
            return false;
        }
        return true;
    }

    private void OnDrawGizmos()
    {
        if(Application.isPlaying)
        {
            Gizmos.color = Color.yellow;
            Vector3 topLeftCorner = new Vector3(midPointX - camWidth / 2f, 0f, midPointZ + camHeight / 2f);
            Vector3 topRightCorner = new Vector3(midPointX + camWidth / 2f, 0f, midPointZ + camHeight / 2f);
            Vector3 bottomLeftCorner = new Vector3(midPointX - camWidth / 2f, 0f, midPointZ - camHeight / 2f);
            Vector3 bottomRightCorner = new Vector3(midPointX + camWidth / 2f, 0f, midPointZ - camHeight / 2f);
            Gizmos.DrawLine(topLeftCorner, topRightCorner);
            Gizmos.DrawLine(topRightCorner, bottomRightCorner);
            Gizmos.DrawLine(bottomRightCorner, bottomLeftCorner);
            Gizmos.DrawLine(bottomLeftCorner, topLeftCorner);
            Gizmos.DrawSphere(new Vector3(midPointX, 0f, midPointZ), 1f);
        }
    }
}
