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

    private void SpawnSelectedPlayers()
    {
        foreach (var player in PlayerObjectHandler.playerControllers)
        {
            var playerController = PlayerObjectHandler.playerControllers[player.Key];
            var playerObjectName = PlayerObjectHandler.playerSelectionNames[player.Key];
            var playerControlScheme = PlayerObjectHandler.playerControlSchemes[player.Key];

            GameObject parentPlayerObject = new GameObject();

            for (int i = 0; i < playerObjectName.Count; i++)
            {
                var currentObject = Resources.Load<GameObject>(playerObjectName[i]);

                // Only activate PlayerInput component on the first object (it defines the "player")
                if (i == 0)
                {
                    parentPlayerObject = currentObject;
                    PlayerInput playerInput = PlayerInput.Instantiate(currentObject, player.Key, playerControlScheme, -1, playerController);

                    // Activates the player input component on the prefab we just instantiated
                    // We have the component disabled by default, otherwise it could not be a "selectable object" independent of the PlayerInput component on the cursor
                    // in the selection screen
                    currentObject.GetComponent<PlayerMovement>().SetPlayerInputActive(true, playerInput);

                    //  *** It seems...that the above Instantiation doesn't exactly work... I'm assuming, because the PlayerInput component on the prefab is starting off
                    // disabled, that it...doesn't work.  This code here will force it to keep the device/scheme/etc... that we tried to assign the wretch above!
                    var inputUser = playerInput.user;
                    playerInput.SwitchCurrentControlScheme(playerControlScheme);
                    InputUser.PerformPairingWithDevice(playerController, inputUser, InputUserPairingOptions.UnpairCurrentDevicesFromUser);
                }

                // If not the first object (sword/vehicle/etc...) just instantiate, don't associate a PlayerInput
                else
                {
                    Instantiate(currentObject, parentPlayerObject.transform);
                }
            }
        }
    }
}