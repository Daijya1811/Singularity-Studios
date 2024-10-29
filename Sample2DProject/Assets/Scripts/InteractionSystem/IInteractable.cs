// InteractionAllowEnum to allow for certain types of interactions
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
    
    //used to determine who can interact with an object
    public InteractionAllowed WhoCanInteract { get; }
    
    //flag for UI prompt change
    bool PromptUpdated { get; set; } 

    public bool Interact(Interactor interactor);
}
