using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;

// Credit for this class goes to Unity Forums user @Holonet: https://discussions.unity.com/u/holonet/summary
// https://github.com/GeneralProtectionFault/InputLocalMultiplayerTemplate/tree/master, https://www.youtube.com/watch?v=nP2V35CNmNI&t=1567s
// A small amount of modifications went into making this template fit our project's specific needs, as well as added more comments to the code. 

/// <summary>
/// Spawns the players (with their respective character selections) when the gameplay scene initializes
/// </summary>
public class SceneInitialization : MonoBehaviour
{
    bool firstSpawnDone = false;
    private void OnEnable()
    {
        SceneManager.activeSceneChanged += SceneBeginCheck;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= SceneBeginCheck;
    }

    /// <summary>
    /// If transitioning from the Character Selection scene, spawn the players! 
    /// </summary>
    /// <param name="fromScene"> Character Selection Scene. </param>
    /// <param name="toScene"> Gameplay Scene. </param>
    private void SceneBeginCheck(Scene fromScene, Scene toScene)
    {
        if (PlayerObjectHandler.shouldSpawnSelectedPlayers)
        {
            SpawnSelectedPlayers();
            PlayerObjectHandler.shouldSpawnSelectedPlayers = false;
        }
    }
    /// <summary>
    /// Spawns the selected characters into the gameplay scene. 
    /// </summary>
    private void SpawnSelectedPlayers()
    {
        foreach (var player in PlayerObjectHandler.playerControllers)
        {
            InputDevice playerController = PlayerObjectHandler.playerControllers[player.Key];
            List<string> playerObjectName = PlayerObjectHandler.playerSelectionNames[player.Key];
            string playerControlScheme = PlayerObjectHandler.playerControlSchemes[player.Key];

            for (int i = 0; i < playerObjectName.Count; i++)
            {
                GameObject currentObject = Resources.Load<GameObject>(playerObjectName[i]);

                // Spawn the players in their specific starting positions
                if (!firstSpawnDone) { currentObject.transform.position = new Vector3(10.8f, 0f, -4.6f); currentObject.transform.rotation = Quaternion.Euler(0f, 150f, 0f); firstSpawnDone = true; }
                else { currentObject.transform.position = new Vector3(10.4f, 0f, 1.1f); currentObject.transform.rotation = Quaternion.Euler(0f, 140f, 0f); }

                PlayerInput playerInput = PlayerInput.Instantiate(currentObject, player.Key, playerControlScheme, -1, playerController);

                // Activates the player input component on the prefab we just instantiated
                // We have the component disabled by default, otherwise it could not be a "selectable object" independent of the PlayerInput component on the cursor
                // in the selection screen
                currentObject.GetComponent<PlayerMovement>().SetPlayerInputActive(true, playerInput);

                //  *** It seems...that the above Instantiation doesn't exactly work... I'm assuming, because the PlayerInput component on the prefab is starting off
                // disabled, that it...doesn't work.  This code here will force it to keep the device/scheme/etc... that we tried to assign the wretch above!
                InputUser inputUser = playerInput.user;
                playerInput.SwitchCurrentControlScheme(playerControlScheme);
                InputUser.PerformPairingWithDevice(playerController, inputUser, InputUserPairingOptions.UnpairCurrentDevicesFromUser);
            }
        }
    }
}