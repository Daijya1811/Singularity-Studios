using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("CharacterSelectionScene");
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