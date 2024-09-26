using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float playerSpeed = 20f;
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
            movement = new Vector3(movementInput.x, 0f, movementInput.y) * sprintMultiplier;
        }
        else movement = new Vector3(movementInput.x, 0f, movementInput.y);
        rb.velocity += movement * playerSpeed * Time.deltaTime;
        transform.LookAt(transform.position + movement);

        if (rb.velocity.magnitude > 2f)
        {
            animator.SetBool("isRunning", true);
        }
        else animator.SetBool("isRunning", false);

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
