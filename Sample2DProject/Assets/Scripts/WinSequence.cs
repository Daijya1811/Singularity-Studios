using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class WinSequence : MonoBehaviour
{
    [SerializeField] Fader winFader;

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
        GetComponent<PlayableDirector>().Play();
        GetComponentInChildren<Animator>().enabled = true;
    }

    void ReturnToMainMenu(PlayableDirector pd)
    {
        SceneManager.LoadScene(0);
    }
}
