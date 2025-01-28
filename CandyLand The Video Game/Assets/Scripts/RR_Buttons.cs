using UnityEngine;

public class RR_Buttons : MonoBehaviour {
    [SerializeField] MM_Buttons mainButtons;
    bool ready = false;

    public void readyUp() {
        ready = !ready;
        if (ready) mainButtons.readyUps += 1; 
        else mainButtons.readyUps -= 1;
        mainButtons.readyText.text = "Players Ready? " + mainButtons.readyUps;
	}
}
