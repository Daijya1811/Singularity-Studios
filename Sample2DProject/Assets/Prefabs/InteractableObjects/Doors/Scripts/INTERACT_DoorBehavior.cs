using System;
using UnityEngine;

public class DoorBehavior : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    
    [Header("Animator Related Fields")]
    [SerializeField] private Animator animator;
    [SerializeField] private string triggerOpenName = "OpenDoor";
    [SerializeField] private string triggerClosedName = "CloseDoor";

    [Header("Lighting")] 
    [SerializeField] private Light doorLight;
    [SerializeField] private Color unlockedColor;
    [SerializeField] private Color lockedColor;
    [SerializeField] private Color transitionColor;
    [SerializeField] private MeshRenderer screen;
    private Material mat;



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
        mat = screen.sharedMaterial;
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
        }
        else
        {
            animator.SetTrigger(triggerClosedName);
            isOpen = false;
            prompt = "Open";
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
        }
    }
}
