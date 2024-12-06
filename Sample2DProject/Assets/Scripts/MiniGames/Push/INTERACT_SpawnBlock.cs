using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlock : MonoBehaviour, IInteractable
{
    [SerializeField] string prompt = "Press Button";
    [SerializeField] private InteractionAllowed interactionAllowed = InteractionAllowed.Both;
    [SerializeField] private GameObject blockToSpawn;
    [SerializeField] private Vector3 spawnPos;
    private bool pressed = false;

    public string InteractionPrompt => prompt;

    public InteractionAllowed WhoCanInteract => interactionAllowed;

    //only use if prompt is updated
    public bool PromptUpdated { get; set; }

    public bool Interact(Interactor interactor)
    {
        if (pressed) return false;
        Instantiate(blockToSpawn, spawnPos, Quaternion.identity);
        pressed = true;
        prompt = "";
        PromptUpdated = true;
        return true;
    }
}
