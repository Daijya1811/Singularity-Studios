using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    /*private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Jon Test Scene")
        {
            AudioManager.instance.GetComponent<AudioSource>().Pause();
        }
    }*/
}
