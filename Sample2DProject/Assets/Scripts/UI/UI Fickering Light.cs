using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFickeringLight : MonoBehaviour
{
    public Light lightOB;

    public AudioSource lightSound;

    public float minTime;
    public float maxTime;
    public float timer;

    [SerializeField] private bool consistent = false;
    [SerializeField] private float timeToFlickerConsistent;


    void Start()
    {
        timer = 0.5f;
    }

    void Update()
    {
        LightsFlickering();
    }

    void LightsFlickering()
    {
        
        
        if (timer > 0)
            timer -= Time.deltaTime;
        
        
        if (timer <= 0)
        {
            lightOB.enabled = !lightOB.enabled;
            lightSound.Play();
            if (consistent)
            {
                timer = timeToFlickerConsistent;
                return;
            }
            timer = Random.Range(minTime, maxTime);
        }
    }
}
