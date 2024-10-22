using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script makes it where the character select screen is always loaded first when pressing play, for Quality of Life purposes. 
/// </summary>
public class LoadCharacterSelect : MonoBehaviour
{
    private void Start()
    {
        GameObject coopManager = GameObject.Find("Co-Op Manager");
        if(coopManager == null) SceneManager.LoadScene("CharacterSelectionScene");
    }
}
