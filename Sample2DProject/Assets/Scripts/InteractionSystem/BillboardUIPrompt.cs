using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BillboardUIPrompt : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private Canvas uIPanel;

    [SerializeField] private Image buttonPrompt;
    [SerializeField] private Sprite[] interactButtons;
    private string deviceManufacturer;
    private string deviceName;

    const int PlayStationPrompt = 0;
    const int XboxPrompt = 1;
    const int KeyboardPrompt = 2;

    
    private Camera playerSplitScreenCamera;

    private Camera mainCam;

    private bool isDisplayed;

    public bool IsDisplayed
    {
        get { return isDisplayed; }
    }

    private void Start()
    {
        uIPanel = GetComponent<Canvas>();
        uIPanel.enabled = false;
        mainCam = Camera.main;
        playerSplitScreenCamera = transform.parent.parent.GetComponent<UnityEngine.InputSystem.PlayerInput>().camera;

        deviceName = transform.parent.parent.GetComponent<UnityEngine.InputSystem.PlayerInput>().devices[0].name;
        deviceManufacturer = transform.parent.parent.GetComponent<UnityEngine.InputSystem.PlayerInput>().devices[0].description.manufacturer;
    }

    private void LateUpdate()
    {
        Quaternion rotation = mainCam.enabled ? mainCam.transform.rotation : playerSplitScreenCamera.transform.rotation; 
        transform.LookAt(transform.position + rotation * Vector3.forward,
            rotation * Vector3.up);
    }

    /// <summary>
    /// Sets up the UI panel
    /// </summary>
    /// <param name="promptText">The text that the panel Displays</param>
    public void SetUp(string promptText)
    {
        if (promptText == "")
        {
            this.promptText.text = promptText;
            buttonPrompt.enabled = false;
        }

        else
        {
            buttonPrompt.enabled = true;
            if (deviceManufacturer != null && deviceManufacturer.Equals("Sony Interactive Entertainment"))
            {
                buttonPrompt.sprite = interactButtons[PlayStationPrompt];
            }
            else if (deviceName != null && deviceName.Equals("Keyboard"))
            {
                buttonPrompt.sprite = interactButtons[KeyboardPrompt];
            }
            else
            {
                buttonPrompt.sprite = interactButtons[XboxPrompt];
            }

            this.promptText.text = "Press      to " + promptText;
        }
        uIPanel.enabled = true;
        isDisplayed = true;
    }

    /// <summary>
    /// Closes the UI
    /// </summary>
    public void Close()
    {
        uIPanel.enabled = false;
        isDisplayed = false;
    }
}
