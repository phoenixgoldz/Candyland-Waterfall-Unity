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
        if (readyUps > 0) { 
            SceneManager.LoadScene("CandlyLandscape");
            PlayerPrefs.SetInt("players", readyUps);
            PlayerPrefs.SetInt("player1name", );
            PlayerPrefs.SetInt("player1color", );
			PlayerPrefs.SetInt("player2name", );
			PlayerPrefs.SetInt("player2color", );
			PlayerPrefs.SetInt("player3name", );
			PlayerPrefs.SetInt("player3color", );
			PlayerPrefs.SetInt("player4name", );
			PlayerPrefs.SetInt("player4color", );
			PlayerPrefs.SetInt("player5name", );
			PlayerPrefs.SetInt("player5color", );
			PlayerPrefs.SetInt("player6name", );
			PlayerPrefs.SetInt("player6color", );
			PlayerPrefs.Save();
        }
    }
}