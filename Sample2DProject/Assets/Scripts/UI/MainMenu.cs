using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

public class Menu : MonoBehaviour
{
    private void Start()
    {
        PlayerObjectHandler.playerControllers.Clear();
        PlayerObjectHandler.playerSelectionNames.Clear();
        PlayerObjectHandler.playerControlSchemes.Clear();
        StickToPlayer.playerCameraReferences.Clear();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitClicked()
    {
        UnityEngine.Application.Quit();
    }

    public GameObject button;
    public void DisableButton()
    {
        button.SetActive(false);
    }

    public void EnableButton()
    {
        button.SetActive(true);
    }
}