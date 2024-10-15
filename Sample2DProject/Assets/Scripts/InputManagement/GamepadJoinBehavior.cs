using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Credit for this class goes to Unity Forums user @Holonet: https://discussions.unity.com/u/holonet/summary
// https://github.com/GeneralProtectionFault/InputLocalMultiplayerTemplate/tree/master, https://www.youtube.com/watch?v=nP2V35CNmNI&t=1567s
// A small amount of modifications went into making this template fit our project's specific needs, as well as added more comments to the code. 

/// <summary>
/// This class handles detecting player inputs and adding the player cursors to Character Selection. 
/// </summary>
public class GamepadJoinBehavior : MonoBehaviour
{
    // Reference to the Character Selection canvas initialized in the editor
    [SerializeField] Canvas currentCanvas;

    // Start is called before the first frame update
    void Start()
    {
        // If any button is pressed, detect a new user and dynamically add them as a player. 
        InputAction myAction = new InputAction(binding: "/*/<button>");
        myAction.performed += (action) =>
        {
            //UnityEngine.Debug.Log(Gamepad.current.description.deviceClass);
            //UnityEngine.Debug.Log(action.control.device.description);
            AddPlayer(action.control.device);
        };
        myAction.Enable();
    }

    /// <summary>
    /// Adds in a new player if input is detected. This will spawn a cursor for them to select a character. 
    /// </summary>
    /// <param name="device"> The input device that was detected. </param>
    void AddPlayer(InputDevice device)
    {
        // Avoid running if the device is already paired to a player
        foreach (var player in PlayerInput.all)
        {
            foreach (var playerDevice in player.devices)
            {
                if (device == playerDevice)
                {
                    return;
                }
            }
        }

        // Don't execute if not a gamepad or keyboard
        if (!device.displayName.Contains("Controller") && !device.displayName.Contains("Keyboard") && !device.displayName.Contains("Gamepad"))
            return;

        // Keep track of player index (1-based). This is also needed to load the proper cursor. 
        int playerNumberToAdd = PlayerInput.all.Count + 1;

        // Getting controlScheme argument from the player to pass into the PlayerInput.Instantiate() method. 
        string controlScheme = "";
        if (device.displayName.Contains("Controller") || device.displayName.Contains("Gamepad"))
            controlScheme = "Gamepad";
        else if (device.displayName.Contains("Keyboard"))
            controlScheme = "Keyboard";

        // *** Note this utilizes the NAME of the cursor prefabs to associate the player/player # ***
        // Loads in from the Resources folder and creates the cursor. DO NOT TOUCH THE RESOURCES FOLDER. 
        GameObject playerCursor = Resources.Load<GameObject>($"CursorPrefabs/P{playerNumberToAdd}_Cursor");
        if (!playerCursor.activeInHierarchy)
        {
            // This creates the PlayerInput component.
            // In Unity's new input system, the creation of this component is what defines the existence of the "player"
            PlayerInput theCursor = PlayerInput.Instantiate(playerCursor, -1, controlScheme, -1, device);
            theCursor.transform.parent = currentCanvas.transform;
            theCursor.transform.localScale = new Vector3(1f, 1f, 1f);
        }

    }
}