using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerTurnUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text playerTurnText;
    public Button drawButton;
    public Image drawnCardImage;
    public GameObject pauseMenuUI;

    [Header("Game Settings")]
    public Sprite defaultCardSprite;

    [Header("Audio Settings")]
    public AudioClip[] gameSongs; // Array of songs
    public AudioClip drawCardSound;
    public AudioClip buttonClickSound;
    public AudioSource audioSource;

    private bool isPaused = false;
    private int currentSongIndex = 0;

    void Start()
    {
        // Ensure pause menu is inactive at start
        pauseMenuUI.SetActive(false);

        // Set up button listeners
        drawButton.onClick.AddListener(DrawCard);

        // Start playing the first song
        PlaySong(currentSongIndex);
    }

    void Update()
    {
        // Handle pause menu toggle
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        // Handle song skipping
        if (Input.GetKeyDown(KeyCode.P))
        {
            SkipToNextSong();
        }

        // Handle song looping
        if (!audioSource.isPlaying && !isPaused)
        {
            SkipToNextSong();
        }
    }

    public void UpdatePlayerTurn(string playerName)
    {
        playerTurnText.text = $"{playerName}'s Turn";
    }

    void DrawCard()
    {
        // Play sound effect for drawing a card
        PlaySound(drawCardSound);

        // Implement card drawing logic
        Sprite drawnCard = GetRandomCardSprite();
        drawnCardImage.sprite = drawnCard;
    }

    Sprite GetRandomCardSprite()
    {
        return defaultCardSprite;
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);

        // Pause or resume the game
        Time.timeScale = isPaused ? 0 : 1;

        // Pause or resume the music
        if (isPaused)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }

        // Play button click sound
        PlaySound(buttonClickSound);
    }

    public void ContinueGame()
    {
        TogglePauseMenu();
    }

    public void ExitToMainMenu()
    {
        // Ensure the game unpauses when exiting
        Time.timeScale = 1;

        // Stop all music
        audioSource.Stop();

        // Load the main menu
        SceneManager.LoadScene("StartMenuScene");

        // Play button click sound
        PlaySound(buttonClickSound);
    }

    void PlaySong(int songIndex)
    {
        if (gameSongs.Length > 0)
        {
            audioSource.clip = gameSongs[songIndex];
            audioSource.loop = false; // Disable loop for individual songs
            audioSource.Play();
        }
    }

    void SkipToNextSong()
    {
        if (gameSongs.Length > 0)
        {
            currentSongIndex = (currentSongIndex + 1) % gameSongs.Length; // Loop through the array
            PlaySong(currentSongIndex);
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
