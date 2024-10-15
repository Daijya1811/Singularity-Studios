using TMPro;
using UnityEngine;

public class BillboardUIPrompt : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private GameObject uIPanel;
    
    private Camera playerSplitScreenCamera;

    private Camera mainCam;

    private bool isDisplayed;

    public bool IsDisplayed
    {
        get { return isDisplayed; }
    }

    private void Start()
    {
        uIPanel.SetActive(false);
        mainCam = Camera.main;
        playerSplitScreenCamera = transform.parent.parent.GetComponent<UnityEngine.InputSystem.PlayerInput>().camera;
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
        this.promptText.text = promptText;
        uIPanel.SetActive(true);
        isDisplayed = true;
    }

    /// <summary>
    /// Closes the UI
    /// </summary>
    public void Close()
    {
        uIPanel.SetActive(false);
        isDisplayed = false;
    }
}
