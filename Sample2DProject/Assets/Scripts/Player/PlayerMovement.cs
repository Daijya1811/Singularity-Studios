using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class handles the player's movement (which is animation based). 
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float lerpSmoothingSpeed = 2f;
    [SerializeField] float floatingHeight = 2f;
    bool toggleSprint;
    bool isSprinting;
    float speed;

    Vector2 movementInput = Vector2.zero;
    Animator animator;
    PlayerInput pi;
    bool isAlreadySelected = false;

    public bool IsAlreadySelected { get { return isAlreadySelected; } set { isAlreadySelected = value; } }

    void Awake()
    {
        animator = GetComponent<Animator>();
        pi = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PlayerInput>().enabled)
        {
            Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y);
            AnimateMovement(movement, isSprinting);
        
            // This line is necessary for player to look at last input direction!
            movement += transform.forward;
            // Smooth rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Reads a Vector2 normalized input of the Move keys to see which direction the player should move. 
    /// </summary>
    /// <param name="context"> The normalized Vector2 value of the movement. </param>
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    /// <summary>
    /// Checks to see if the sprint button is "started," "performed," or "released." 
    /// </summary>
    /// <param name="context"> The state of the Sprint input. </param>
    public void OnSprint(InputAction.CallbackContext context)
    {
        if(toggleSprint)
        {
            isSprinting = !isSprinting;
        }
        else isSprinting = context.performed;
    }

    /// <summary>
    /// Animates the player to smoothly move within their animation blend tree. 
    /// </summary>
    /// <param name="movement"> movement input. </param>
    /// <param name="isSprinting"> bool value to determine if the player is sprinting or not. </param>
    private void AnimateMovement(Vector3 movement, bool isSprinting)
    {
        float multiplier = isSprinting ? 2f : 1.25f;
        float target = movement.magnitude * multiplier;

        // Prevent speed from becoming a negative number. 
        if (speed < 0f) speed = 0f;

        // If running against a wall, do not play a running animation. 
        if(Physics.Raycast(transform.position, transform.forward, 0.25f, 3))
        {
            speed -= lerpSmoothingSpeed * Time.deltaTime;
        }

        // Prevents weird lerp jitters if speed and target are the same values.
        else if (Mathf.Abs(speed - target) < 0.1f)
        {
            speed = target;
        }

        // Lerping the movement for smooth animation blending
        else if (speed < target)
        {
            speed += lerpSmoothingSpeed * Time.deltaTime;
        }
        else if (speed > target)
        {
            speed -= lerpSmoothingSpeed * Time.deltaTime;
        }

        if (speed > 2.0f) speed = 2.0f; //had to cap at 2, sometimes was 2.0000001 which is funny
        animator.SetFloat("speed", speed);
    }
    /// <summary>
    /// Enables the Player Input components on the players after the character selection scene transitions into gameplay.
    /// </summary>
    /// <param name="activation"> bool value to enable or disable to Player Input component. </param>
    /// <param name="playerInput"> Reference to the Player Input component. </param>
    public void SetPlayerInputActive(bool activation, PlayerInput playerInput)
    {
        if (pi == null) pi = playerInput;
        pi.enabled = activation;
    }
    /// <summary>
    /// Toggles the player's to have an offset in their Y-positions to make it look like they are floating in gravity. 
    /// </summary>
    /// <param name="isFloating"> bool value to determine if player is floating or not. </param>
    private void ToggleFloating(bool isFloating)
    {
        if(isFloating)
        {
            transform.position = new Vector3(transform.position.x, floatingHeight, transform.position.z);
        }
        else transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }
}
