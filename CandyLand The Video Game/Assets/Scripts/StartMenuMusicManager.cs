using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuMusicManager : MonoBehaviour
{
    private static StartMenuMusicManager instance; // Singleton instance
    private AudioSource audioSource;

    void Awake()
    {
        // Ensure only one instance of the music manager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    void Start()
    {
        PlayMusic();
    }

    void Update()
    {
        // Check if the current scene is "CandyLandscape" and stop the music
        if (SceneManager.GetActiveScene().name == "CandyLandscape" && audioSource.isPlaying)
        {
            StopMusic();
        }
    }

    public void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            // Restore the audio playback position if it was saved
            if (PlayerPrefs.HasKey("AudioTime"))
            {
                audioSource.time = PlayerPrefs.GetFloat("AudioTime");
            }
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            // Save the current playback position
            PlayerPrefs.SetFloat("AudioTime", audioSource.time);
            PlayerPrefs.Save();
            audioSource.Stop();
        }
    }

    void OnDisable()
    {
        // Save the playback position when the object is disabled
        if (audioSource != null && audioSource.isPlaying)
        {
            PlayerPrefs.SetFloat("AudioTime", audioSource.time);
            PlayerPrefs.Save();
        }
    }
}
