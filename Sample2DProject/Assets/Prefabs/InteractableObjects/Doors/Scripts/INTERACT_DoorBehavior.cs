using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    
    [Header("Animator Related Fields")]
    [SerializeField] private Animator animator;
    [SerializeField] private string triggerOpenName = "OpenDoor";
    [SerializeField] private string triggerClosedName = "CloseDoor";
    private bool isOpen;

    private void Start()
    {
        animator = GetComponent<Animator>();
        isOpen = false;
    }

    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        if (animator.IsInTransition(0)) return true;
        if (!isOpen)
        {
            animator.SetTrigger(triggerOpenName);
            isOpen = true;
        }
        else
        {
            animator.SetTrigger(triggerClosedName);
            isOpen = false;
        }
        
        return true;
    }
}
