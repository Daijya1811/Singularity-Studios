using UnityEngine;
using UnityEngine.UI;

public class FlashlightUI : MonoBehaviour
{
    private FlashlightController flashlightController;
    private Image image;
    private bool imageEnabled;

    [SerializeField] private float disabledAlpha = .1f;

    private void Start()
    {
        image = GetComponent<Image>();
        flashlightController = FindObjectOfType<FlashlightController>();
        imageEnabled = image.enabled;

        if (flashlightController) ToggleFlashLightUI();

        else image.enabled = false;

    }

    private bool ToggleFlashLightUI()
    {
        imageEnabled = flashlightController.FlashlightEnabled;
        Color color = image.color;

        color.a = disabledAlpha;
        
        image.color = color;
        
        return imageEnabled;
    }

    private void Update()
    {
        //if something went wrong and it isn't present
        if (!flashlightController) return;

        image.fillAmount = flashlightController.CurrentBatteryTime / flashlightController.TotalBatteryTime;
        
        //if the image is not active, ignore further logic
        if (!ToggleFlashLightUI()) return;
        
        Color color = image.color;

        color.a = Mathf.Clamp01(flashlightController.CurrentBatteryTime / flashlightController.TotalBatteryTime);
        
        image.color = color;

        
    }
}
