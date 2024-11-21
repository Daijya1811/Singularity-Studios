using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;

/// <summary>
/// This script removes player control during cutscenes. 
/// </summary>
[RequireComponent(typeof(PlayableDirector))]
public class CinematicControlRemover : MonoBehaviour
{
    [SerializeField] GameObject planet;
    GameObject[] players;
    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }
    private void OnEnable()
    {
        GetComponent<PlayableDirector>().played += DisableControl;
        GetComponent<PlayableDirector>().stopped += EnableControl;
    }
    private void OnDisable()
    {
        GetComponent<PlayableDirector>().played -= DisableControl;
        GetComponent<PlayableDirector>().stopped -= EnableControl;
    }
    void DisableControl(PlayableDirector pd)
    {
        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerInput>().SwitchCurrentActionMap("DisableInput");
        }
    }
    void EnableControl(PlayableDirector pd)
    {
        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        }
        planet.transform.localPosition = new Vector3(25f, -22f, 0f);
    }
}