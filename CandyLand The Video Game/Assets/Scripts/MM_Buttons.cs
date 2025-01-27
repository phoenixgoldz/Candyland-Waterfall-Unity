using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MM_Buttons : MonoBehaviour {
    [SerializeField] TMP_InputField[] names;
    [SerializeField] TMP_Text[] colors;
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
            PlayerPrefs.SetString("player1name", names[0].text);
            PlayerPrefs.SetString("player1color", "red");
			PlayerPrefs.SetString("player2name", names[1].text);
			PlayerPrefs.SetString("player2color", "yellow");
			PlayerPrefs.SetString("player3name", names[2].text);
			PlayerPrefs.SetString("player3color", "green");
			PlayerPrefs.SetString("player4name", names[3].text);
			PlayerPrefs.SetString("player4color", "purple");
			PlayerPrefs.SetString("player5name", names[4].text);
			PlayerPrefs.SetString("player5color", "orange");
			PlayerPrefs.SetString("player6name", names[5].text);
			PlayerPrefs.SetString("player6color", "blue");
			PlayerPrefs.Save();
        }
    }
}