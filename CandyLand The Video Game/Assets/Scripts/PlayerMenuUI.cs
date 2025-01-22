using UnityEngine;

public class PlayerMenuUi : MonoBehaviour {
    [SerializeField] Player[] players;

    public struct Player {
        /// <summary>
        /// just basic player info
        /// </summary>
        /// <param name="pieceType">type of marker used to rep</param>
        /// <param name="playerNum">player position</param>
        /// <param name="playerName">name of player</param>
        /// <param name="playerReady">whether player is ready</param>
        public Player(int pieceType, int playerNum, string playerName, bool playerReady) {
            piece = pieceType;
            num = playerNum;
            name = playerName;
            ready = playerReady;
        }

        int piece { get; }
        int num { get; }
        string name { get; }
        bool ready { get; }
    }

    void Start() {
        
    }

    void Update() {
        
    }

    public void readyClick(int pos) {
        
    }

    public void startClick() {
    
    }
}