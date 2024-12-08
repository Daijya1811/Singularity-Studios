using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hacking
{
    /// <summary>
    /// This script checks for if the Brain Player interacts with the Hacking Console. If an interaction is detected, then the split screen cameras get toggled, 
    /// and the Brain Player's Action Map switches over from Player to Hacking (transferring input over to the minigame's Player Triangle from the Player). 
    /// </summary>
    public class StartHacking : MonoBehaviour, IInteractable
    {
        [SerializeField] DoorBehavior doorToUnlock;
        public string prompt = "Hack";
        [SerializeField] private InteractionAllowed interactionAllowed = InteractionAllowed.Brain;
        public InteractionAllowed WhoCanInteract => interactionAllowed;
        
        //only use if prompt is updated
        public bool PromptUpdated { get; set; } 
        
        GameObject hackingScene;

        [Header("Player who has interacted info:")]
        Interactor interactorInstance;
        CamerasController cameraController;

        bool thisHackInstanceHasWon = false;
        public bool HackInstanceHasWon { set { thisHackInstanceHasWon = value; } }

        public string InteractionPrompt => prompt;

        [SerializeField] int amountOfBlockersFirstGame;
        [SerializeField] int amountOfBlockersSecondGame;

        BlockerNodesSpawner blockerSpawner;
        BadNodesSpawner badSpawner;
        CountdownTimer countdownTimer;

        private void Awake()
        {
            cameraController = FindObjectOfType<CamerasController>();
            hackingScene = GameObject.FindGameObjectWithTag("Hacking");
            blockerSpawner = FindObjectOfType<BlockerNodesSpawner>();
            badSpawner = FindObjectOfType<BadNodesSpawner>();
            countdownTimer = FindObjectOfType<CountdownTimer>();
        }
        private void Start()
        {
            hackingScene.SetActive(false);
        }

        public bool Interact(Interactor interactor)
        {
            // IDEA: Stick player triangle in each player as a child, but disabled. When interaction, split the cameras, have the player camera go to the minigame, enable triangle
            // When done, stick the camera back to the player mesh after re-enabling player mesh. 

            // Check to see if the player is the Brain and not the Brawn. If so, start hacking!
            if(interactor.GetComponentInChildren<PlayerTriangleMovement>() != null && !thisHackInstanceHasWon)
            {
                interactorInstance = interactor;
                PlayerTriangleMovement playerTriangle = interactorInstance.GetComponentInChildren<PlayerTriangleMovement>();
                playerTriangle.DoorToUnlock = doorToUnlock;
                playerTriangle.HackTerminal = this;
                ToggleMiniGameOn(interactorInstance);
                SetHackingDifficulty(playerTriangle);
                interactor.GetComponentInChildren<HackingFinished>().InteractorInstance = interactorInstance;
                return true;
            }

            return false;
        }
        void ToggleMiniGameOn(Interactor interactorInstance)
        {
            int pi = interactorInstance.GetComponent<PlayerInput>().playerIndex;
            cameraController.ToggleCamera();
            StickToPlayer.playerCameraReferences[pi].IsInMiniGame = true;
            interactorInstance.GetComponentInChildren<PlayerTriangleMovement>().enabled = true;
            interactorInstance.GetComponentInChildren<PlayerTriangleMovement>().GetComponent<MeshRenderer>().enabled = true;
            hackingScene.SetActive(true);
            interactorInstance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Hacking");
        }
        void SetHackingDifficulty(PlayerTriangleMovement playerTriangle)
        {
            if (playerTriangle.TimesWon == 0)
            {
                countdownTimer.TimeForTask = 30f;
                countdownTimer.IsHackingActive = true;
                badSpawner.SpawnNodes(amountOfBlockersFirstGame);
                blockerSpawner.SpawnNodes(amountOfBlockersFirstGame);
            }
            else if(playerTriangle.TimesWon == 1)
            {
                countdownTimer.TimeForTask = 15f;
                badSpawner.SpawnNodes(amountOfBlockersSecondGame);
                blockerSpawner.SpawnNodes(amountOfBlockersSecondGame);
            }
        }
    }
}