using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Credit for this class goes to Unity Forums user @Holonet: https://discussions.unity.com/u/holonet/summary
// https://github.com/GeneralProtectionFault/InputLocalMultiplayerTemplate/tree/master, https://www.youtube.com/watch?v=nP2V35CNmNI&t=1567s
// A small amount of modifications went into making this template fit our project's specific needs, as well as added more comments to the code. 

/// <summary>
/// This class helps keep reference to which player picked which character, and loads into the gameplay scene. 
/// </summary>
public class PlayerObjectHandler : MonoBehaviour
{
    // In order to preserve the player's selections AND the controllers they used, and transfer the PlayerInput component
    // from the cursor to the selection for gameplay, we'll use dictionaries to keep track of both, destroy the cursors and
    // selected objects, then reinstantiate

    public static Dictionary<int, InputDevice> playerControllers = new Dictionary<int, InputDevice>();

    // List of the NAMES of the game objects that we want to instantiate as the "player" when getting that scene
    // ***  List<string> is because we might want to have more than 1 game object for a player (i.e. a character and a sword)
    //      In this case, we'll treat the FIRST object as the one w/ the PlayerInput, and just instantiate the rest
    //      Parenting/placing/etc... can be performed after the scene is loaded.

    public static Dictionary<int, List<string>> playerSelectionNames = new Dictionary<int, List<string>>();
    public static Dictionary<int, string> playerControlSchemes = new Dictionary<int, string>();

    // Use this only if persisting cursors to the next screen (if selecting more than one "thing"/object per player)
    // If so, set shouldSpawnSelectedPlayers to false.  We'll just keep the cursors, which will have the PlayerInput components (which define the players)
    public static GameObject[] playerCursors;

    [SerializeField] public static bool shouldSpawnSelectedPlayers = false;
    [SerializeField] public static bool shouldPersistCursors = false;


    private void OnEnable()
    {
        CursorBehavior.DoneSelectingEvent += PlayersDoneSelecting;
    }

    private void OnDisable()
    {
        CursorBehavior.DoneSelectingEvent -= PlayersDoneSelecting;
    }

    /// <summary>
    /// Once character selection is done, save the characters to be used to the correct playerIndex. 
    /// Save all the references (from the cursor to the characters) in a dictionary. 
    /// Load the next scene.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PlayersDoneSelecting(object sender, EventArgs e)
    {
        // First, make sure all players have selected their character
        playerCursors = GameObject.FindGameObjectsWithTag("PlayerCursor");

        foreach (var cursor in playerCursors)
        {
            if (!cursor.GetComponent<CursorBehavior>().objectSelected)
            {
                UnityEngine.Debug.Log(cursor + " object has not selected a player!");
                return;
            }
        }

        // If all have selected a character, store the character(s)
        for (int i = 0; i < playerCursors.Length; i++)
        {
            PlayerInput playerInputComponent = playerCursors[i].GetComponent<PlayerInput>();
            string playerSelection = playerCursors[i].GetComponent<CursorBehavior>().playerSelection.name;

            int playerIndex = playerInputComponent.playerIndex;


            playerControllers.Add(playerIndex, playerInputComponent.devices[0]);

            // If initializing/first player object/selection screen
            if (!playerSelectionNames.ContainsKey(playerIndex))
            {
                playerSelectionNames.Add(playerIndex, new List<string>() { playerSelection });
            }
            else
            {
                var currentGameObjectList = playerSelectionNames[playerIndex];
                currentGameObjectList.Add(playerSelection);
            }

            playerControlSchemes.Add(playerInputComponent.playerIndex, playerInputComponent.currentControlScheme);
        }

        // Set this variable if we're done selecitng player objects, and the next scene will be where they spawn.
        shouldSpawnSelectedPlayers = true;

        // Set this if going through multiple selection screens, in which case we'll want to NOT spawn the players
        // (as in, this should always be set the opposite of shouldSpawnSelectedPlayers), and bring the cursors
        // with their PlayerInput components over to the next selection screen.
        shouldPersistCursors = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}