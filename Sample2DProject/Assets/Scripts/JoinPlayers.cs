using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoinPlayers : MonoBehaviour
{
    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private PlayerInputManager _playerInputManager;

    private void Start()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();
        _playerInputManager.playerPrefab = playerPrefabs[0];
        _playerInputManager = GetComponent<PlayerInputManager>();
        
    }

    public void SwapPrefab()
    {
        _playerInputManager.playerPrefab = playerPrefabs[1];
    }
    

}



