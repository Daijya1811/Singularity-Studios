using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ClickPlayerMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    private Input mouseInput;

    private Camera cam;
    
    private static readonly int GroundLayer = 1 << 6;

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
    // Update is called once per frame
    void Update()
    {
      
    }
}
