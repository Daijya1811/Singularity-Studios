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
    GameObject[] players;
    [SerializeField] BoxCollider cryoPod1InteractTrigger;
    [SerializeField] BoxCollider cryoPod2InteractTrigger;
    [SerializeField] GameObject skipCutsceneText;


    private bool donePlaying = true;
    public bool DonePlaying { get { return donePlaying; } }

    private void Start()
    {
        donePlaying = false;
        //players = GameObject.FindGameObjectsWithTag("Player");
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
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerInput>().SwitchCurrentActionMap("DisableInput");
        }
        cryoPod1InteractTrigger.enabled = false;
        cryoPod2InteractTrigger.enabled = false;
    }
    void EnableControl(PlayableDirector pd)
    {
        donePlaying = true;
        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        }
        cryoPod1InteractTrigger.enabled = true;
        cryoPod2InteractTrigger.enabled = true;
        skipCutsceneText.SetActive(false);
    }
}