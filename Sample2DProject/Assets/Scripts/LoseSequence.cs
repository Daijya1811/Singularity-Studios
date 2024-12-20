using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// When the planet reaches its maximum scale, this script triggers the lose sequence. 
/// The screen fades, and the scene changes back to the main menu. 
/// </summary>
public class LoseSequence : MonoBehaviour
{
    PlanetScaler planet;
    [SerializeField] Fader loseFader;

    private void Awake()
    {
        planet = FindObjectOfType<PlanetScaler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(planet.SizeLimitReached)
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        yield return loseFader.FadeRoutine(1f, loseFader.FadeDuration);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
        yield return null;
    }
}
