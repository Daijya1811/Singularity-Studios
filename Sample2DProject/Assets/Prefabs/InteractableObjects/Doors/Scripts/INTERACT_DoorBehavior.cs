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
    
    [Header("Locked")]
    [SerializeField] private bool isLocked = false;

    //unlock the door
    public void Unlock()
    {
        isLocked = false;
    }
    
    //lock the door
    public void Lock()
    {
        isLocked = true;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        isOpen = false;
    }

    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        if (isLocked) return true;
        if (animator.IsInTransition(0)) return true;
        if (!isOpen)
        {
            animator.SetTrigger(triggerOpenName);
            isOpen = true;
            prompt = "Close";
        }
        else
        {
            animator.SetTrigger(triggerClosedName);
            isOpen = false;
            prompt = "Open";
        }
        
        return true;
    }
}
