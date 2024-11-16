using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingMenu : MonoBehaviour
{
    public void SetVolume(float volume)
    {
        
    }

    public void OnBackClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
