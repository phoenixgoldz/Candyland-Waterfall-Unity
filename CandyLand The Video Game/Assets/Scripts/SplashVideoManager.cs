using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SplashVideoManager : MonoBehaviour
{
    public string nextSceneName = "MainMenu"; // Replace with the name of your main menu scene

    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd; // Trigger when the video finishes
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName); // Load the main menu
    }
}
