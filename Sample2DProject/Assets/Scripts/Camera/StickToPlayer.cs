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
    [SerializeField] GameObject miniGameScene;

    public static Dictionary<int, StickToPlayer> playerCameraReferences = new Dictionary<int, StickToPlayer>();
    bool isInMiniGame = false;
    Transform playerTransform;
    ScreenShake screenShake;

    public bool IsInMiniGame { get { return isInMiniGame; } set { isInMiniGame = value; } }

    private void Awake()
    {
        screenShake = FindObjectOfType<ScreenShake>();
    }
    private void Start()
    {
        Camera thisCamera = GetComponent<Camera>();
        PlayerInput[] playerInputs = FindObjectsOfType<PlayerInput>();

        // Determine which player to follow!
        if(playerInputs[0].camera == null)
        {
            playerInputs[0].camera = thisCamera;
            playerCameraReferences.Add(playerInputs[0].playerIndex, this);
            playerTransform = playerInputs[0].transform;
        }
        else if(playerInputs.Length > 1)
        {
            playerInputs[1].camera = thisCamera;
            playerCameraReferences.Add(playerInputs[1].playerIndex, this);
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
        // Camera should follow the player if they are not doing a minigame
        if (!isInMiniGame) FollowPlayer();
        else if (isInMiniGame) WatchMiniGame();
    }
    void FollowPlayer()
    {
        if (playerTransform != null)
        {
            this.transform.position = new Vector3(playerTransform.position.x,
                                                  playerTransform.position.y + posOffsetY,
                                                  playerTransform.position.z + posOffsetZ);
            this.transform.rotation = Quaternion.Euler(45, 0, 0);
        }

        if(screenShake.CanShake())
        {
            screenShake.ShakeScreen(this.gameObject, screenShake.ShakeIntensity / 25f);
        }
    }
    void WatchMiniGame()
    {
        this.transform.position = new Vector3(miniGameScene.transform.position.x,
                                              miniGameScene.transform.position.y,
                                              miniGameScene.transform.position.z - 25f);
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        miniGameScene.GetComponentInChildren<Canvas>().worldCamera = this.GetComponent<Camera>();
    }
}
