using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoinPlayers : MonoBehaviour
{
    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private PlayerInputManager _playerInputManager;

    [SerializeField] GameObject playerModel1;
    [SerializeField] GameObject playerModel2;
    private GameObject player1;
    private GameObject player2;
    Dictionary<int, GameObject> players = new Dictionary<int, GameObject>();
    int playerCount = 0;
    private void Start()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();
        //_playerInputManager.playerPrefab = playerPrefabs[0];
        // _playerInputManager = GetComponent<PlayerInputManager>();
        
    }
    // Method to set the first parent object
    public void SetPlayer1(GameObject newParent)
    {
        player1 = newParent;
    }

    // Method to set the second parent object
    public void SetPlayer2(GameObject newParent)
    {
        player2 = newParent;
    }

    // Method to spawn a child model under a specified parent
    public void SpawnChildModel(int playerIndex, bool usePlayerModel1)
    {
        // Set the selectedParent object as either player1 or player2 based on the playerIndex
        GameObject selectedParent = playerIndex == 0 ? player1 : player2;
        // Set the selectedModel as either using the first or second model
        GameObject selectedModel = usePlayerModel1 ? playerModel1 : playerModel2;

        if (selectedParent != null)
        {
            // Instantiate the selected child prefab
            GameObject childObject = Instantiate(selectedModel, selectedParent.transform.position, Quaternion.identity);

            // Set the child object's parent
            childObject.transform.SetParent(selectedParent.transform, false);
        }
        else
        {
            Debug.LogError("Selected parent object not set. Make sure it's instantiated before spawning the child.");
        }
    }

    public void SwapPrefab()
    {
        _playerInputManager.playerPrefab = playerPrefabs[1];
    }
}



