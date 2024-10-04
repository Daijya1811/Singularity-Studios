using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] [Range(1, 2)] float sprintMultiplier = 1.5f;
    [SerializeField] float floatingHeight = 2f;
    bool isSprinting;

    Vector2 movementInput = Vector2.zero;
    Rigidbody rb;
    Animator animator;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;

        // If sprinting, transition to the sprinting animation state! 
        if (isSprinting)
        {
            animator.SetBool("isSprinting", true);
            movement = new Vector3(movementInput.x, 0f, movementInput.y) * sprintMultiplier;
        }
        else 
        {
            animator.SetBool("isSprinting", false);
            movement = new Vector3(movementInput.x, 0f, movementInput.y);
        }

        // If not sprinting, but moving, transition to the running animation state! 
        if (movement.magnitude > 0)
        {
            animator.SetBool("isRunning", true);
        }
        // If not moving, transition to the idle animation state!
        else if (movement.magnitude < 0.05f) animator.SetBool("isRunning", false);

        // This line is necessary for player to look at last input direction!
        movement += transform.forward;
        // Smooth rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed * Time.deltaTime);
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
        isSprinting = context.performed;
    }

    /// <summary>
    /// Toggles the player's to have an offset in their Y-positions to make it look like they are floating in gravity. 
    /// </summary>
    /// <param name="isFloating"> bool value to determine if player is floating or not. </param>
    private void ToggleFloating(bool isFloating)
    {
        if(isFloating)
        {
            rb.position = new Vector3(rb.position.x, floatingHeight, rb.position.z);
        }
        else rb.position = new Vector3(rb.position.x, 0f, rb.position.z);
    }
}
