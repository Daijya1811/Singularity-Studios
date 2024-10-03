using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    
    public string InteractionPrompt => prompt;
    
    public bool Interact(Interactor interactor)
    {
       Debug.Log("Event Triggered");
       return true;
    }
}
