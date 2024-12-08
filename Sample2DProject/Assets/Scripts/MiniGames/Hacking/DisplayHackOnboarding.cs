using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hacking
{
    public class DisplayHackOnboarding : MonoBehaviour
    {
        [SerializeField] private Image P3Keyboard;
        [SerializeField] private Image P3Gamepad;
        [SerializeField] private Image P4Keyboard;
        [SerializeField] private Image P4Gamepad;
        [SerializeField] private Sprite[] gamepadButtons;
        [SerializeField] float crossFadeSpeed = 3f;

        GameObject playerTriangle;
        BillboardUIPrompt deviceInfo;
        MeshRenderer mesh;
        private string deviceManufacturer;
        private string deviceName;

        const int PlayStationPrompt = 0;
        const int XboxPrompt = 1;

        private void OnEnable()
        {
            playerTriangle = GameObject.FindGameObjectWithTag("PlayerTriangle");
            deviceInfo = playerTriangle.transform.parent.GetComponent<Interactor>().InteractionUI;
            mesh = playerTriangle.GetComponent<MeshRenderer>();
            deviceManufacturer = deviceInfo.DeviceManufacturer;
            deviceName = deviceInfo.DeviceName;

            if (deviceManufacturer != null && deviceManufacturer.Equals("Sony Interactive Entertainment"))
            {
                P3Keyboard.enabled = false;
                P4Keyboard.enabled = false;
                P3Gamepad.sprite = gamepadButtons[PlayStationPrompt];
                P4Gamepad.sprite = gamepadButtons[PlayStationPrompt];
            }
            else if (deviceName != null && deviceName.Equals("Keyboard"))
            {
                P3Gamepad.enabled = false;
                P4Gamepad.enabled = false;
            }
            else
            {
                P3Keyboard.enabled = false;
                P4Keyboard.enabled = false;
                P3Gamepad.sprite = gamepadButtons[XboxPrompt];
                P4Gamepad.sprite = gamepadButtons[XboxPrompt];
            }
        }

        private void Update()
        {
            if (P4Keyboard.enabled) FadingUI(P4Keyboard);
            else FadingUI(P4Gamepad);

            if (!mesh.enabled) return;
            if (mesh.enabled) mesh.enabled = false;
        }

        void FadingUI(Image image)
        {
            float oscillatingAlpha = 0.5f * (Mathf.Sin(Time.time - (Mathf.PI / 2)) + 1); //Normalizes oscillations between 0 and 1 instead of -1 and 1
            Color newColor = new Color(image.color.r, image.color.g, image.color.b, oscillatingAlpha);
            image.color = newColor;
        }
    }
}
