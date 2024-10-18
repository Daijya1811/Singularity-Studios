using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightCharger : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private VoidEventChannelSO flashlightChargeEventSO;
    
    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        flashlightChargeEventSO.RaiseEvent(null);
        return true;
    }
}
