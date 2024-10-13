using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject midCameraPrefab;
    GameObject[] players;

    /// <summary>
    /// Instantiates a GameObject midCameraPrefab which represents the midpoint position of both players. 
    /// The midCameraPrefab also has bounding boxes associated with it which prevent the players from leaving the camera screen. 
    /// </summary>
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
            transform.position = new Vector3(player1Pos.x, player1Pos.y, player1Pos.z);
        }
        else if (players.Length == 2)
        {
            Vector3 player1Pos = players[0].transform.position;
            Vector3 player2Pos = players[1].transform.position;
            float midPointX = (player2Pos.x + player1Pos.x) / 2f;
            float midPointY = (player2Pos.y + player1Pos.y) / 2f;
            float midPointZ = (player2Pos.z + player1Pos.z) / 2f;
            transform.position = new Vector3(midPointX, midPointY, midPointZ);
            midCameraPrefab.transform.position = new Vector3(midPointX, midPointY, midPointZ);
        }
        else
        {
            transform.position = Vector3.zero;
        }
    }
}
