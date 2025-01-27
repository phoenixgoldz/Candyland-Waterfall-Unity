using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PlayerMenuUI : MonoBehaviour
{
    [Header("Player Panels")]
    [SerializeField] private GameObject[] playerPanels; // Panels for P1 to P6
    [SerializeField] private GameObject[] addPlayerButtons; // Add Player Buttons for P3 to P6

    [Header("Player Models")]
    private readonly string[] playerModelNames = { "RedModel", "YellowModel", "GreenModel", "PurpleModel", "OrangeModel", "BlueModel" }; // Default model names for each player

    private int activePlayers = 2; // Minimum 2 players to start

    void Start()
    {
        // Initialize panels: Show P1 and P2; Hide others
        for (int i = 0; i < playerPanels.Length; i++)
        {
            playerPanels[i].SetActive(i < activePlayers);
        }

        // Enable Add Player buttons for P3 to P6
        for (int i = 2; i < addPlayerButtons.Length; i++)
        {
            addPlayerButtons[i].SetActive(true);
        }
    }
  

    public void AddPlayer(int playerIndex)
    {
        if (playerIndex >= 2 && playerIndex < playerPanels.Length && activePlayers < 6)
        {
            activePlayers++;
            playerPanels[playerIndex].SetActive(true);
            addPlayerButtons[playerIndex - 2].SetActive(false); // Disable the corresponding Add Player button
        }
    }

    public void PlayerReady(int playerIndex)
    {
        // Update the UI to indicate that the player is ready
        Text readyText = playerPanels[playerIndex].transform.Find("ReadyupText").GetComponent<Text>();
        readyText.text = "Ready!";
        Debug.Log($"Player {playerIndex + 1} is ready!");
    }

    public void StartGame()
    {
        if (AllPlayersReady())
        {
            Debug.Log("Starting Game with " + activePlayers + " players!");

            // Store the number of active players and their models for the next scene
            PlayerPrefs.SetInt("ActivePlayers", activePlayers);
            for (int i = 0; i < activePlayers; i++)
            {
                PlayerPrefs.SetString($"Player{i + 1}Model", playerModelNames[i]);
            }

            // Load the next scene
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            Debug.Log("Not all players are ready!");
        }
    }

    private bool AllPlayersReady()
    {
        for (int i = 0; i < activePlayers; i++)
        {
            Text readyText = playerPanels[i].transform.Find("ReadyupText").GetComponent<Text>();
            if (readyText.text != "Ready!")
                return false;
        }
        return true;
    }
}
