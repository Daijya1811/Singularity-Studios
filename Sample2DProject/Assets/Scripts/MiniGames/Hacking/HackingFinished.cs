using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hacking
{
    public class HackingFinished : MonoBehaviour
    {
        Interactor interactorInstance;
        CamerasController cameraController;
        GameObject hackingScene;
        public Interactor InteractorInstance { set { interactorInstance = value; } }
        private void Awake()
        {
            cameraController = FindObjectOfType<CamerasController>();
            hackingScene = GameObject.FindGameObjectWithTag("Hacking");
        }

        public void ToggleMiniGameOff()
        {
            int pi = interactorInstance.GetComponent<PlayerInput>().playerIndex;
            cameraController.ToggleCamera();
            StickToPlayer.playerCameraReferences[pi].IsInMiniGame = false;
            interactorInstance.GetComponentInChildren<PlayerTriangleMovement>().enabled = false;
            interactorInstance.GetComponentInChildren<PlayerTriangleMovement>().GetComponent<MeshRenderer>().enabled = false;
            hackingScene.SetActive(false);
            interactorInstance.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");
        }
    }
}
