using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OscillateButtonAlpha : MonoBehaviour
{
    [SerializeField] float oscillationSpeed = 3f;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float oscillatingAlpha = 0.5f * (Mathf.Sin((Time.time * oscillationSpeed) - (Mathf.PI / 2)) + 1); //Normalizes oscillations between 0 and 1 instead of -1 and 1
        Color newColor = new Color(image.color.r, image.color.g, image.color.b, oscillatingAlpha);
        image.color = newColor;
    }
}