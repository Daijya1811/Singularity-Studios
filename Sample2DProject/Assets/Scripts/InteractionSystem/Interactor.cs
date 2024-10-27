using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [Header("Serialized Fields")]
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius= 0.5f;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private BillboardUIPrompt interactionUI;
    
    [Header("Serialized : Code Assigned")]
    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private bool isBrawn;

    public bool IsBrawn
    {
        get { return isBrawn; }
    }

    private InputAction interactAction;
    private IInteractable interactable;

    //colliders
    private readonly Collider[] colliders = new Collider[3]; // 3 max

    //number of colliders found
    [Header("Editor Display : Do Not Edit")] [SerializeField] private int numFound;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        interactAction = playerInput.actions["Player/Interact"];
    }
    
    private void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders,
            interactableMask);

        //check if interaction, if so, enable ui and check for input
        //otherwise close ui
        if (numFound > 0)
        {
            interactable = colliders[0].GetComponent<IInteractable>();
            
            if (interactable != null) 
            {
                //if no ui enable it
                if(!interactionUI.IsDisplayed) interactionUI.SetUp(interactable.InteractionPrompt);
                
                if(interactAction.WasPressedThisFrame())
                    interactable.Interact(this);
            }
        }
        else
        {
            if (interactable != null) interactable = null;
            if(interactionUI.IsDisplayed) interactionUI.Close();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }
}
