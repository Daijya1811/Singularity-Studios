using UnityEngine;
using UnityEngine.UI;

public class FlashlightUI : MonoBehaviour
{
    private FlashlightController flashlightController;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        flashlightController = FindObjectOfType<FlashlightController>();

        if (flashlightController) ToggleFlashLightUI();

        else image.enabled = false;

    }

    private bool ToggleFlashLightUI()
    {
        image.enabled = flashlightController.FlashlightEnabled;

        return image.enabled;
    }

    private void Update()
    {
        //if something went wrong and it isn't present
        if (!flashlightController) return;

        //if the image is not active, ignore further logic
        if (!ToggleFlashLightUI()) return;
        
        Color color = image.color;

        color.a = Mathf.Clamp01(flashlightController.CurrentBatteryTime / flashlightController.TotalBatteryTime);
        
        image.color = color;
    }
}
