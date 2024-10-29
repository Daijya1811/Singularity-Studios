// Define the InteractionAllowed enum
public enum InteractionAllowed
{
    None,         
    Brawn,      
    Brain,   
    Both 
}

public interface IInteractable
{
    public string InteractionPrompt { get; }
    
    public InteractionAllowed WhoCanInteract { get; }
    bool PromptUpdated { get; set; } 

    public bool Interact(Interactor interactor);
}
