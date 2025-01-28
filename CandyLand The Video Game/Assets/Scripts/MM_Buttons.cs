using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MM_Buttons : MonoBehaviour
{
    [SerializeField] GameObject[] playerSelectBgs; // Array for all PlayerSelectBg objects
    [SerializeField] GameObject[] addPlayersButtons; // Array for all AddPlayersButtons
    public int readyUps = 0;
    public TMP_Text readyText;

    // Predefined player colors
    private readonly string[] playerColors = {
        "Red", "Yellow", "Light Green", "Purple", "Orange", "Light Blue"
    };

    public void selectScene()
    {
        SceneManager.LoadScene("PlayersUIScene");
    }

    public void gameScene()
    {
        if (readyUps > 0)
        {
            SceneManager.LoadScene("CandlyLandscape");

            // Save player data
            PlayerPrefs.SetInt("players", readyUps);
        }
    }

    // Function to unhide a specific player's UI and hide its button
    public void ShowPlayerUIAndHideButton(int playerIndex)
    {
        if (playerIndex >= 0 && playerIndex < playerSelectBgs.Length)
        {
            // Unhide the player's UI
            if (playerSelectBgs[playerIndex] != null)
            {
                playerSelectBgs[playerIndex].SetActive(true);
            }

            // Hide the corresponding button
            if (addPlayersButtons[playerIndex] != null)
            {
                addPlayersButtons[playerIndex].SetActive(false);
            }
        }
    }

    // Function to return to the main menu
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene"); // Replace with your actual main menu scene name
    }
}
