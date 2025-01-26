using UnityEngine;
using UnityEngine.SceneManagement;

public class MM_Buttons : MonoBehaviour {
    public int readyUps = 0;

    void Start() {
        
    }

    void Update() {
        
    }

    public void selectScene() {
        SceneManager.LoadScene("PlayersUIScene");
    }

    public void gameScene() {
        if (readyUps > 0)SceneManager.LoadScene("CandlyLandscape");
    }
}