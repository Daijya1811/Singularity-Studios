using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    
    public string InteractionPrompt => prompt;
    
    [SerializeField] private InteractionAllowed interactionAllowed = InteractionAllowed.Both;
    public InteractionAllowed WhoCanInteract => interactionAllowed;
    
    //only use if prompt is updated
    public bool PromptUpdated { get; set; } 
    
    public bool Interact(Interactor interactor)
    {
       Debug.Log("Event Triggered");
       return true;
    }
}
