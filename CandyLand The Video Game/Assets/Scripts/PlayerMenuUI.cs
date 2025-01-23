using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject[] playerPanels; // The UI panels for P1, P2, P3, and P4
    [SerializeField] private string[] availableColors; // List of pawn colors
    private int[] selectedColors; // Tracks selected colors for each player

    private int activePlayers = 2; // Default to 2 players (P1 and P2)

    void Start()
    {
        selectedColors = new int[playerPanels.Length];

        // Initialize player panels
        for (int i = 0; i < playerPanels.Length; i++)
        {
            playerPanels[i].SetActive(i < activePlayers); // Show only the first two panels initially
        }
    }

    // Called when "+" or "-" buttons are clicked to add/remove players
    public void UpdatePlayerCount(int change)
    {
        activePlayers = Mathf.Clamp(activePlayers + change, 2, 4);

        for (int i = 0; i < playerPanels.Length; i++)
        {
            playerPanels[i].SetActive(i < activePlayers);
        }
    }

    // Called when left or right arrow is clicked for a specific player
    public void ChangePawnColor(int playerIndex, int direction)
    {
        if (playerIndex < 0 || playerIndex >= selectedColors.Length) return;

        selectedColors[playerIndex] = (selectedColors[playerIndex] + direction + availableColors.Length) % availableColors.Length;

        // Update the UI (e.g., change the color of the pawn indicator)
        Text playerColorText = playerPanels[playerIndex].transform.Find("PawnColorText").GetComponent<Text>();
        playerColorText.text = availableColors[selectedColors[playerIndex]];
    }

    // Called when "Ready" is clicked for a specific player
    public void PlayerReady(int playerIndex)
    {
        // Handle player ready logic here (e.g., update UI to indicate readiness)
        Debug.Log($"Player {playerIndex + 1} is ready!");
    }

    // Called when the "Start" button is clicked
    public void StartGame()
    {
        if (AllPlayersReady())
        {
            Debug.Log("Starting Game!");
            // Start the game logic
        }
        else
        {
            Debug.Log("Not all players are ready.");
        }
    }

    private bool AllPlayersReady()
    {
        // Check readiness of all active players
        for (int i = 0; i < activePlayers; i++)
        {
            // Assume there's a "Ready" toggle or flag for each player
            Toggle readyToggle = playerPanels[i].transform.Find("ReadyToggle").GetComponent<Toggle>();
            if (!readyToggle.isOn)
                return false;
        }
        return true;
    }
}
