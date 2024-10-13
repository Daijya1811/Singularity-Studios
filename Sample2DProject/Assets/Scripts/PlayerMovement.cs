using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float lerpSmoothingSpeed = 2f;
    [SerializeField] float floatingHeight = 2f;
    bool toggleSprint;
    bool isSprinting;
    float speed;

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
        Vector3 movement = new Vector3(movementInput.x, 0f, movementInput.y);
        AnimateMovement(movement, isSprinting);
        /*movement = Vector3.zero;

        // If sprinting, transition to the sprinting animation state! 
        if (isSprinting)
        {
            animator.SetBool("isSprinting", true);
            movement = new Vector3(movementInput.x, 0f, movementInput.y);
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
        else if (movement.magnitude < 0.05f) animator.SetBool("isRunning", false);*/

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

        // If running against a wall, do not play a running animation. 
        if(Physics.Raycast(transform.position, transform.forward, 0.25f, 3))
        {
            speed -= lerpSmoothingSpeed * Time.deltaTime;
        }

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

        animator.SetFloat("speed", speed);
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
