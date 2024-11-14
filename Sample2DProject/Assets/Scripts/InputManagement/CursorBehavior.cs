using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Credit for this class goes to Unity Forums user @Holonet: https://discussions.unity.com/u/holonet/summary
// https://github.com/GeneralProtectionFault/InputLocalMultiplayerTemplate/tree/master, https://www.youtube.com/watch?v=nP2V35CNmNI&t=1567s
// A small amount of modifications went into making this template fit our project's specific needs, as well as added more comments to the code. 

/// <summary>
/// This class handles picking the Character with the cursor during Character Selection. 
/// </summary>
public class CursorBehavior : MonoBehaviour
{
    private Vector2 movement;
    [SerializeField] private float cursorSpeed;

    private float screenEdgeThreshold = .02f;
    public bool objectSelected = false;
    public GameObject playerSelection;

    public static EventHandler DoneSelectingEvent;

    void Update()
    {
        // Don't allow the cursor past the edge of the screen!
        var viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if ((viewportPosition.x < screenEdgeThreshold && movement.x < 0) ||
            (viewportPosition.x > 1 - screenEdgeThreshold && movement.x > 0) ||
            (viewportPosition.y < screenEdgeThreshold && movement.y < 0) ||
            (viewportPosition.y > 1 - screenEdgeThreshold && movement.y > 0))
            return;

        // Moves the cursor
        transform.Translate(new Vector3(movement.x, movement.y, 0f) * cursorSpeed * Time.deltaTime);
        // UnityEngine.Debug.Log("There are currently " + PlayerInput.all.Count + " players.");
    }
    /// <summary>
    /// Moves the cursor if it hasn't selected a character selection.
    /// </summary>
    /// <param name="context">context to check if WASD/ Left Stick buttons are being pressed. </param>
    public void OnCursorMove(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Canceled && !objectSelected)
            movement = context.ReadValue<Vector2>();
        else  // Released button!
            movement = Vector2.zero;
    }
    /// <summary>
    /// If the cursor is hovering over a character selection, press the Select Button (Spacebar or the A button on a gamepad)
    /// to select that character to be used for that player. 
    /// </summary>
    /// <param name="context">context to check if Spacebar or the A button is being pressed. </param>
    public void OnSelectButton(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            // Debug.DrawRay(transform.position, Vector3.forward, Color.red, 50f);

            RaycastHit target;

            // The cursor does a raycast to see if it hits a Character Selection. If successful, that character gets assigned to the player!
            if (Physics.Raycast(transform.position, Vector3.forward, out target, 1000f, LayerMask.GetMask("PlayerObjects")))
            {
                if (!objectSelected && !target.transform.GetComponent<PlayerMovement>().IsAlreadySelected)
                {
                    objectSelected = true;
                    target.transform.GetComponent<PlayerMovement>().IsAlreadySelected = true;
                    playerSelection = target.transform.gameObject;
                    // Highlight the character when they get selected
                    playerSelection.GetComponentInChildren<Light>().intensity += 1;
                    return;
                }
            }

            // If the player already selected a character, but wants to let go and try to pick a different character,
            // they can press the button again to deselect too! 
            if (objectSelected && target.transform.GetComponent<PlayerMovement>().IsAlreadySelected)
            {
                objectSelected = false;
                target.transform.GetComponent<PlayerMovement>().IsAlreadySelected = false;
                // Darken the character when they get deselected
                playerSelection.GetComponentInChildren<Light>().intensity -= 1;
                playerSelection = null;
            }
        }
    }
    /// <summary>
    /// If both players have chosen their characters, they can press Enter or Start to transition from the character selection scene to gameplay!
    /// </summary>
    /// <param name="context">context to check if Enter or Start have been pressed. </param>
    public void OnStartButton(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            DoneSelectingEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}