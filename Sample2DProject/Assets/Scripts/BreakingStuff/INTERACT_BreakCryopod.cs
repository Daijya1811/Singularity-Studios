using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakCryopod : MonoBehaviour, IInteractable
{
    [SerializeField] string prompt = "SMASH!";
    [SerializeField] private InteractionAllowed interactionAllowed = InteractionAllowed.Brawn;

    public string InteractionPrompt => prompt;
    public InteractionAllowed WhoCanInteract => interactionAllowed;

    //only use if prompt is updated
    public bool PromptUpdated { get; set; }

    public bool Interact(Interactor interactor)
    {
        GetComponentInChildren<Break>().Destroy();
        return true;
    }
}
