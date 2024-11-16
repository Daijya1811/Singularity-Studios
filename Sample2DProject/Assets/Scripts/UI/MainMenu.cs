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

    public void OnCreditClicked()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void OnSettingClicked()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void OnBackClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnQuitClicked()
    {
        UnityEngine.Application.Quit();
    }
}