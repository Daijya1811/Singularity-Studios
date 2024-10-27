using System.Collections;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [Header("Material Settings and Colors")]
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Color incorrectColor;
    [SerializeField] private Color correctColor;
    [SerializeField] private bool isCorrect = false;
    
    private Material mat;
    
 
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        mat = meshRenderer.material;
        if(!isCorrect)
            mat.SetColor("_Color", incorrectColor);
        else
            mat.SetColor("_Color", correctColor);
            
    }

    //change material color
    public void CorrectMaterialColor()
    {
        isCorrect = true;
        mat.SetColor("_Color", correctColor);
    }
    
    public void InCorrectMaterialColor()
    {
        isCorrect = false;
        mat.SetColor("_Color", incorrectColor);
    }
}
