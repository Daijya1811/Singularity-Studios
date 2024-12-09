using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


[RequireComponent(typeof(PlayableDirector))]
public class StopCinematic : MonoBehaviour
{
    [SerializeField] GameObject skipCutscene;
    bool hasSkipped;
    public void SkipCutscene()
    {
        if (hasSkipped) return;
        GetComponent<PlayableDirector>().Stop();
        skipCutscene.SetActive(false);
        hasSkipped = true;
    }
}
