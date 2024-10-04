using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] float offsetY = 30f;
    [SerializeField] float offsetZ = -30f;
    [SerializeField] GameObject midCameraPrefab;
    GameObject[] players;

    /// <summary>
    /// Instantiates a GameObject midCameraPrefab which represents the midpoint position of both players. 
    /// The midCameraPrefab also has bounding boxes associated with it which prevent the players from leaving the camera screen. 
    /// </summary>
    void Start()
    {
        midCameraPrefab = Instantiate(midCameraPrefab, this.transform);
        midCameraPrefab.transform.localEulerAngles = new Vector3(-45, 0, 0);
        players = GetPlayers();
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
            return;
            /*Vector3 player1Pos = players[0].transform.position;
            transform.position = new Vector3(player1Pos.x, player1Pos.y + offsetY, player1Pos.z + offsetZ);*/
        }
        else if (players.Length == 2)
        {
            Vector3 player1Pos = players[0].transform.position;
            Vector3 player2Pos = players[1].transform.position;
            float midPointX = (player2Pos.x + player1Pos.x) / 2f;
            float midPointY = (player2Pos.y + player1Pos.y) / 2f;
            float midPointZ = (player2Pos.z + player1Pos.z) / 2f;
            transform.position = new Vector3(midPointX, midPointY + offsetY, midPointZ + offsetZ);
            midCameraPrefab.transform.position = new Vector3(midPointX, midPointY, midPointZ);
        }
    }
}
