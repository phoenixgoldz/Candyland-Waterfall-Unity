using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public List<playerPawn> players;
    [SerializeField] public board board;
    CardManager cardManager = new CardManager();
    GameState gameState;
    playerPawn activePlayer;
    private bool drawled = false;
    void Start() {
        players = new List<playerPawn>(PlayerPrefs.GetInt("players"));
        for (int i = 0; i < players.Count; i++) {
            
        }
        //gameState = GameState.STARTSCREEN;
        gameState = GameState.INGAME;
        activePlayer = players[0];
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("active players turn " + activePlayer.turn);
        Debug.Log("Drawled" + drawled);
        Debug.Log("Active Player" + activePlayer);
        switch (gameState)
        {
            case GameState.STARTSCREEN:
                break;
            case GameState.PLAYERSELECT:
                break;
            case GameState.GAMESTART:
                break;
            case GameState.INGAME:
                if (activePlayer.turn)
                {

                    if (!activePlayer.skipTurn)
                    {
                        if (drawled)
                        {
                            card drawnCard = cardManager.drawCard();
                            Debug.Log(drawnCard.color);
                            activePlayer.currentTile = board.Move(activePlayer, drawnCard);
                            Debug.Log(activePlayer.currentTile.color);
                            activePlayer.GetComponentInParent<Transform>().transform.position = activePlayer.currentTile.transform.position;
                            activePlayer.turn = false;
                            drawled = false;
                        }
                    }
                    else
                    {
                        activePlayer.skipTurn = false;
                        activePlayer.turn = false;
                    }

                }
                else {
                    NextPlayer();
                }
                break;
            case GameState.GAMEOVER:
                break;
        }
    }
    public void NextPlayer()
    {
        for (int i = 0; i < players.Count; i++)
        {
            playerPawn p = players[i];
            if (p == activePlayer)
            {
                    if (i < players.Count - 2)
                    {
                        activePlayer = players[i + 1];
                    }
                    else
                    {
                        activePlayer = players[0];
                    }
                activePlayer.turn = true;
                    return;
            }
        }
    }

    public void onDrawPress()
    {
        drawled = true;
    }

    enum GameState
    { 
        STARTSCREEN,PLAYERSELECT,GAMESTART,INGAME,GAMEOVER
    }
}
