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
    bool isSprinting;

    Vector2 movementInput = Vector2.zero;
    Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.performed;
    }
    /*private bool CheckForOtherPlayer()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length == 2) return true;
        else return false;
    }*/
}
