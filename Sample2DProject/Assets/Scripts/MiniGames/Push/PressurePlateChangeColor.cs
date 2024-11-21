using System.Collections;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [Header("Material Settings and Colors")]
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Color incorrectColor;
    [SerializeField] private Color correctColor;
    [SerializeField] private bool isCorrect = false;

    public bool IsCorrect
    {
        get { return isCorrect; }
    }

    [Header("RAISE Event Channel for Pressure Plate")] 
    [SerializeField] private VoidEventChannelSO voidEventChannelSo;
    
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

    /// <summary>
    /// Change the color of the pressure plate and raise an event that it got pushed.
    /// </summary>
    public void CorrectMaterialColor()
    {
        isCorrect = true;
        mat.shader = Shader.Find("Standard");//swap shader
        mat.SetColor("_Color", correctColor);
        voidEventChannelSo.RaiseEvent();
    }
    
    public void InCorrectMaterialColor()
    {
        isCorrect = false;
        mat.SetColor("_Color", incorrectColor);
    }
}
