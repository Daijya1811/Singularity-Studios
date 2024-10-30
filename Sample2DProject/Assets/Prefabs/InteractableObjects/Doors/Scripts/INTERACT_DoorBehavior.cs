using System;
using UnityEngine;

public class DoorBehavior : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    
    [Header("Animator Related Fields")]
    [SerializeField] private Animator animator;
    [SerializeField] private string triggerOpenName = "OpenDoor";
    [SerializeField] private string triggerClosedName = "CloseDoor";
    
    [SerializeField] private InteractionAllowed interactionAllowed = InteractionAllowed.Both;
    
    //only use if prompt is updated
    public bool PromptUpdated { get; set; }

    public InteractionAllowed WhoCanInteract => interactionAllowed;

    [Header("Lighting")] 
    [SerializeField] private Light doorLight;
    [SerializeField] private Color unlockedColor;
    [SerializeField] private Color lockedColor;
    [SerializeField] private Color transitionColor;
    [SerializeField] private MeshRenderer screen;
    [SerializeField] private MeshRenderer screenBack;
    private Material mat;
    private Material matBack;



    private bool isOpen;
    
    [Header("Locked")]
    [SerializeField] private bool isLocked = false;

    //unlock the door
    public void Unlock()
    {
        isLocked = false;
        interactionAllowed = InteractionAllowed.Both;
    }
    
    //lock the door
    public void Lock()
    {
        isLocked = true;
        interactionAllowed = InteractionAllowed.None;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        isOpen = false;
        mat = screen.material;
        matBack = screenBack.material;
        if(isLocked) interactionAllowed = InteractionAllowed.None;
        SetLightColor();
    }

    public string InteractionPrompt => prompt;

    
    /// <summary>
    /// Sets the trigger to open or close the door if not moving and not locked
    /// </summary>
    /// <param name="interactor"></param>
    /// <returns></returns>
    public bool Interact(Interactor interactor)
    {
        if (isLocked || animator.IsInTransition(0)) return true;
        if (!isOpen)
        {
            animator.SetTrigger(triggerOpenName);
            isOpen = true;
            prompt = "Close";
            PromptUpdated = true;
        }
        else
        {
            animator.SetTrigger(triggerClosedName);
            isOpen = false;
            prompt = "Open";
            PromptUpdated = true;
        }

        return true;
    }

    private void Update()
    {
        SetLightColor();
    }

    /// <summary>
    /// change the color of the door based on locked. Optimized to ignore if the same color
    /// </summary>
    private void SetLightColor()
    {
        Color targetColor = isLocked ? lockedColor : unlockedColor;
        if (animator.IsInTransition(0))
        {
            targetColor = transitionColor;
        }

        if (doorLight.color != targetColor)
        {
            doorLight.color = targetColor;
            mat.SetColor("_EmissionColor", targetColor);
            matBack.SetColor(("_EmissionColor"), targetColor);
        }
    }
}
