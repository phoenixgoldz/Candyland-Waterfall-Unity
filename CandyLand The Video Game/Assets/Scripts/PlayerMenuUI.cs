using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuUI : MonoBehaviour
{
    [Header("Player Panels")]
    [SerializeField] private GameObject[] playerPanels; // Panels for P1, P2, P3, P4
    [SerializeField] private GameObject addPlayerButtonP3;
    [SerializeField] private GameObject addPlayerButtonP4;

    [Header("Pawn Colors")]
    [SerializeField] private string[] availableColors; // Colors for selection
    private int[] selectedColors; // Track selected colors for each player

    private int activePlayers = 2; // Minimum 2 players to start

    void Start()
    {
        selectedColors = new int[playerPanels.Length];

        // Initialize panels: Show P1, P2; Hide P3, P4
        for (int i = 0; i < playerPanels.Length; i++)
        {
            playerPanels[i].SetActive(i < activePlayers);
        }

        // Show add player buttons for P3 and P4
        addPlayerButtonP3.SetActive(true);
        addPlayerButtonP4.SetActive(true);
    }

    public void AddPlayer(int playerIndex)
    {
        if (playerIndex == 2 && activePlayers < 3)
        {
            activePlayers++;
            playerPanels[2].SetActive(true);
            addPlayerButtonP3.SetActive(false); // Hide P3 Add Button
        }
        else if (playerIndex == 3 && activePlayers < 4)
        {
            activePlayers++;
            playerPanels[3].SetActive(true);
            addPlayerButtonP4.SetActive(false); // Hide P4 Add Button
        }
    }
    
    public void PlayerReady(int playerIndex)
    {
        // Find ready text and toggle ready state
        Text readyText = playerPanels[playerIndex].transform.Find("ReadyupText").GetComponent<Text>();
        readyText.text = "Ready!";
        Debug.Log($"Player {playerIndex + 1} is ready!");
    }

    public void StartGame()
    {
        if (AllPlayersReady())
        {
            Debug.Log("Starting Game with " + activePlayers + " players!");
            // Implement game start logic
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
