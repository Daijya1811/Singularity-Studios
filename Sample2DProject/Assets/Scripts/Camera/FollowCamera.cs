using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class allows the main camera to always follow the midpoint position between both players. 
/// </summary>
public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject midCameraPrefab;
    GameObject[] players;

    [SerializeField] private Vector3 offset;

    ScreenShake screenShake;

    public Vector3 MidCamPos { get { return midCameraPrefab.transform.position; } }
    public GameObject[] Players { get { return players; } }

    private void Awake()
    {
        screenShake = GetComponent<ScreenShake>();
    }
    void Start()
    {
        players = GetPlayers();
    }

    // Resets the camera within the editor at the end of play
    private void OnDisable()
    {
        midCameraPrefab.transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // Keep calling the GetPlayers function in case the second player hasn't joined yet. 
        if(players.Length < 2)
            players = GetPlayers();
        CenterCamera();

        // If can shake the camera, then shake the camera
        screenShake.ShakeScreen(midCameraPrefab, screenShake.ShakeIntensity);
    }

    /// <summary>
    /// Finds the Player GameObjects tagged with "Player" and stores them in an array to use for reference. 
    /// </summary>
    /// <returns> Array of our Player GameObjects. </returns>
    private GameObject[] GetPlayers()
    {
        return GameObject.FindGameObjectsWithTag("Player");
    }
    
    /// <summary>
    /// Centers the camera on the midpoint position of both players. 
    /// </summary>
    private void CenterCamera()
    {
        if (players.Length == 1) 
        {
            Vector3 player1Pos = players[0].transform.position;
            midCameraPrefab.transform.position = new Vector3(player1Pos.x + offset.x , player1Pos.y + offset.y, player1Pos.z + offset.z );
            transform.position = midCameraPrefab.transform.position;
        }
        else if (players.Length == 2)
        {
            Vector3 player1Pos = players[0].transform.position;
            Vector3 player2Pos = players[1].transform.position;
            float midPointX = (player2Pos.x + player1Pos.x) / 2f;
            float midPointY = (player2Pos.y + player1Pos.y) / 2f;
            float midPointZ = (player2Pos.z + player1Pos.z) / 2f;
            midCameraPrefab.transform.position = new Vector3(midPointX  + offset.x, midPointY + offset.y, midPointZ + offset.z);
            transform.position = midCameraPrefab.transform.position;
        }
        else
        {
            transform.position = Vector3.zero;
        }
    }
}
