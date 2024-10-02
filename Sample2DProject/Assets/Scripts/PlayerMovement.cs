using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 5f;
    [SerializeField] [Range(1, 2)]
    float sprintMultiplier = 1.5f;
    [SerializeField]
    float floatingHeight = 2f;
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
        Vector3 movement;
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
        // Leave this out because we are just going to use the root motion of the animation to move our character
        // rb.velocity += movement * playerSpeed * Time.deltaTime;


        if (movement.magnitude > 0 && !isSprinting)
        {
            animator.SetBool("isRunning", true);
        }
        else if (movement.magnitude < 0.05f) animator.SetBool("isRunning", false);

        // This line is necessary for player to look at last input direction!
        movement += transform.forward;
        // Smooth rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed * Time.deltaTime);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.performed;
    }

    private void ToggleFloating(bool isFloating)
    {
        if(isFloating)
        {
            rb.position = new Vector3(rb.position.x, floatingHeight, rb.position.z);
        }
        else rb.position = new Vector3(rb.position.x, 0f, rb.position.z);
    }
    /*private bool CheckForOtherPlayer()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 2) return true;
        else return false;
    }*/
}
