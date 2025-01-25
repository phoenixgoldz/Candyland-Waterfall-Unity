using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerTurnUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text playerTurnText; // Reference to display whose turn it is
    public Button drawButton; // Button to draw a card
    public Image drawnCardImage; // Image to show drawn card
    public GameObject pauseMenuUI; // Reference to the pause menu UI

    [Header("Game Settings")]
    public Sprite defaultCardSprite; // Default sprite for the drawn card image

    private bool isPaused = false; // Tracks if the game is paused

    void Start()
    {
        // Ensure pause menu is inactive at start
        pauseMenuUI.SetActive(false);

        // Set up button listeners
        drawButton.onClick.AddListener(DrawCard);
    }

    void Update()
    {
        // Open/close pause menu when ESC is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void UpdatePlayerTurn(string playerName)
    {
        playerTurnText.text = $"{playerName}'s Turn";
    }

    void DrawCard()
    {
        // Implement your logic to draw a card and set its image
        Sprite drawnCard = GetRandomCardSprite(); // Replace this with your card drawing logic
        drawnCardImage.sprite = drawnCard;
    }

    Sprite GetRandomCardSprite()
    {
        // Placeholder logic for drawing a random card
        // Replace with your game’s card drawing logic
        return defaultCardSprite;
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);

        // Pause the game if paused
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void ContinueGame()
    {
        TogglePauseMenu();
    }

    public void ExitToMainMenu()
    {
        // Ensure the game unpauses when exiting
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenuScene");
    }
}
