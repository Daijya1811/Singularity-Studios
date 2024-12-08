using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class WinSequence : MonoBehaviour
{
    int numPlayersInWinTrigger = 0;
    private void OnEnable()
    {
        GetComponent<PlayableDirector>().stopped += ReturnToMainMenu;
    }
    private void OnDisable()
    {
        GetComponent<PlayableDirector>().stopped -= ReturnToMainMenu;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") numPlayersInWinTrigger++;
        if (numPlayersInWinTrigger < 2) return; 
        else
        {
            GetComponent<PlayableDirector>().Play();
            GetComponentInChildren<Animator>().enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") numPlayersInWinTrigger--;
    }

    void ReturnToMainMenu(PlayableDirector pd)
    {
        SceneManager.LoadScene(0);
    }
}
