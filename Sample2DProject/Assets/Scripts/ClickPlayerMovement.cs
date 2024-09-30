using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class ClickPlayerMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    private Input mouseInput;

    [SerializeField] private float speed = 3.5f;
    [SerializeField] private float sprintMultiplier = 2f;

    private Camera cam;
    
    private static readonly int GroundLayer = 1 << 6;


    private bool isSprinting;
    private void Awake()
    {
        mouseInput = new Input();
    }

    private void OnEnable()
    {
        mouseInput.Enable();
    }

    private void OnDisable()
    {
        mouseInput.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
        mouseInput.Player.MouseClick.performed += _ => MouseClick();
    }

    void MouseClick()
    {
        Vector3 mousePosition = mouseInput.Player.MousePosition.ReadValue<Vector2>();
        Ray ray = cam.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.magenta, 1000);

        if (Physics.Raycast((ray), out hit, 1000f, GroundLayer))
        {
            Debug.Log("hit");
            agent.destination = hit.point;
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.performed;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isSprinting) agent.speed  *= sprintMultiplier;
        else agent.speed = speed;
    }
}
