using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// This script plays the intro cinematic for the level. 
/// This script must be used instead of setting the PlayableDirector's PlayOnAwake in the editor because then the Play Event won't be sent out. 
/// </summary>
[RequireComponent(typeof(PlayableDirector))]
public class PlayCinematic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<PlayableDirector>().Play();
    }
}