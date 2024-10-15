using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class makes the split screen camera find a player and stick to them! 
/// </summary>
public class StickToPlayer : MonoBehaviour
{
    [SerializeField] float posOffsetY = 5f;
    [SerializeField] float posOffsetZ = -2f;

    Transform playerTransform;

    private void Start()
    {
        Camera thisCamera = GetComponent<Camera>();
        PlayerInput[] playerInputs = FindObjectsOfType<PlayerInput>();

        // Determine which player to follow!
        if(playerInputs[0].camera == null)
        {
            playerInputs[0].camera = thisCamera;
            playerTransform = playerInputs[0].transform;
        }
        else if(playerInputs.Length > 1)
        {
            playerInputs[1].camera = thisCamera;
            playerTransform = playerInputs[1].transform;
        }

        // Also, clean up any artifacts on the player that might have been brought over from character selection scene.
        GameObject[] characterSelectionLights = GameObject.FindGameObjectsWithTag("CharacterSelectionLight");
        foreach (GameObject light in characterSelectionLights)
        {
            light.SetActive(false);
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        // Camera should follow the player
        if(playerTransform != null)
            this.transform.position = new Vector3(playerTransform.position.x,
                                                  playerTransform.position.y + posOffsetY,
                                                  playerTransform.position.z + posOffsetZ);
    }
}
