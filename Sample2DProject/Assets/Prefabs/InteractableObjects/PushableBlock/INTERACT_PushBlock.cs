using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBlock : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private Rigidbody rgB;
    [SerializeField] private float pushForce = 100f;

    private bool interacted = false;
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
        interacted = true;
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
        Debug.Log(dir);

        rgB.AddForce(dir * pushForce, ForceMode.Impulse);
        return true;
    }

    //Stops the block, and the player on collision
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != 6) return;
        if (!interacted) return;

        rgB.velocity = Vector3.zero;
        rgB.angularVelocity = Vector3.zero;


        rgB.drag = 100;
        interacted = false;
        StartCoroutine(DisableKinematicTemporarily());
    }

    private IEnumerator DisableKinematicTemporarily()
    {
        yield return new WaitForSeconds(1.0f);  
        
        rgB.drag = 0;
        rgB.angularVelocity = Vector3.zero;
        rgB.velocity = Vector3.zero;
        interacted = false;
    }
}
