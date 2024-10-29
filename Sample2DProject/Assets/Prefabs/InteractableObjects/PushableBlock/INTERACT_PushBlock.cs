using System;
using UnityEngine;

public class PushableBlock : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private Rigidbody rgB;
    [SerializeField] private float pushForce = 100f;
    
    public string InteractionPrompt => prompt;
    [SerializeField] private InteractionAllowed interactionAllowed = InteractionAllowed.Brawn;
    public InteractionAllowed WhoCanInteract => interactionAllowed;
    
    //only use if prompt is updated
    public bool PromptUpdated { get; set; } 


    private void Start()
    {
        rgB = GetComponent<Rigidbody>();
    }

    public bool Interact(Interactor interactor)
    {
        //get the direction of the interactor
        Vector3 dir = transform.position - interactor.transform.position;
        dir.y = 0;
        
        // Determine the dominant axis (x or z)
        //x
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
        {
            dir.z = 0;
        }
        else //z
        {
            dir.x = 0;
        }
        
        dir = dir.normalized;


        rgB.AddForce(dir * pushForce, ForceMode.Impulse);
        
        return true;
    }

    //Stops the block, and the player on collision
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != 7) return;
        rgB.velocity = Vector3.zero;
        if (other.rigidbody != null)
        {
            other.rigidbody.velocity = Vector3.zero;
        }
    }
}
