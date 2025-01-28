using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GraphicsSettings : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Button applyButton;
    [SerializeField] private Image confirmationImage;

    private Resolution[] resolutions;
    private int selectedResolutionIndex;

    void Start()
    {
        // Populate resolution dropdown
        PopulateResolutions();

        // Load saved settings
        LoadSettings();

        // Add listeners for buttons
        applyButton.onClick.AddListener(() =>
        {
            ApplySettings();
            ShowConfirmationImage(); // Show image on apply
        });

        // Set up the dropdown listener
        resolutionDropdown.onValueChanged.AddListener(delegate { selectedResolutionIndex = resolutionDropdown.value; });
    }

    void PopulateResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = $"{resolutions[i].width} x {resolutions[i].height}";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        selectedResolutionIndex = currentResolutionIndex;
    }

    void ApplySettings()
    {
        // Apply selected resolution and fullscreen mode
        Resolution selectedResolution = resolutions[selectedResolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, fullscreenToggle.isOn);

        // Save settings to PlayerPrefs
        PlayerPrefs.SetInt("ResolutionIndex", selectedResolutionIndex);
        PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();

        Debug.Log("Settings applied and saved!");
    }

    void ShowConfirmationImage()
    {
        if (confirmationImage != null)
        {
            confirmationImage.gameObject.SetActive(true); // Show the image

            // Hide the image after 2 seconds
            Invoke(nameof(HideConfirmationImage), 2f);
        }
    }

    void HideConfirmationImage()
    {
        if (confirmationImage != null)
        {
            confirmationImage.gameObject.SetActive(false);
        }
    }

    void LoadSettings()
    {
        // Load fullscreen mode
        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen") == 1;
        }

        // Load resolution index
        if (PlayerPrefs.HasKey("ResolutionIndex"))
        {
            selectedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex");
            resolutionDropdown.value = selectedResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }
    }

}
