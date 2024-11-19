using UnityEngine;

public class FlashlightCharger : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private VoidEventChannelSO flashlightChargeEventSO;

    [SerializeField] private InteractionAllowed interactionAllowed = InteractionAllowed.Brain;
    public InteractionAllowed WhoCanInteract => interactionAllowed;
    
    //only use if prompt is updated
    public bool PromptUpdated { get; set; } 
    
    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        flashlightChargeEventSO.RaiseEvent(null);
        return true;
    }
}
