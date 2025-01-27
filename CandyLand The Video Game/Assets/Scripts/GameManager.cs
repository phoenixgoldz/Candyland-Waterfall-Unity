using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //setup
    [SerializeField] public List<playerPawn> players;
    [SerializeField] public board board;
    GameState gameState;
    CardManager cardManager = new CardManager();
    [SerializeField] tile startTile;
    [SerializeField] GameObject playerPrefab;
    
    //player updates
    playerPawn activePlayer;
    private bool drawled = false;
    [SerializeField] TMP_Text TurnNumber;

    //player movement
    private bool moving = false;
    List<tile> tilesToMoveThrough = new List<tile>();
    [SerializeField] float moveSpeed = 0.5f;
    private int currentTileIndex = 0;
    private float moveTimer = 0f;
    private Vector3 startPosition;
    private Vector3 endPosition;

    void Start() {
        players = new List<playerPawn>();
        for (int i = 0; i < PlayerPrefs.GetInt("players"); i++) {
            GameObject Go = Instantiate<GameObject>(playerPrefab, startTile.transform.position, Quaternion.identity);
            playerPawn player = Go.AddComponent<playerPawn>();
            player.cam = Go.GetComponentInChildren<CinemachineCamera>();
            player.currentTile = startTile;
            switch (i) {
                case 1:
                    PlayerPrefs.GetString("player1name");
                    PlayerPrefs.GetString("player1color");
                    break;
                case 2:
					PlayerPrefs.GetString("player2name");
					PlayerPrefs.GetString("player2color");
					break;
                case 3:
					PlayerPrefs.GetString("player3name");
					PlayerPrefs.GetString("player3color");
					break;
                case 4:
					PlayerPrefs.GetString("player4name");
					PlayerPrefs.GetString("player4color");
					break;
                case 5:
					PlayerPrefs.GetString("player5name");
					PlayerPrefs.GetString("player5color");
					break;
                case 6:
					PlayerPrefs.GetString("player6name");
					PlayerPrefs.GetString("player6color");
					break;
            }
            players.Add(player);
            
        }
        //gameState = GameState.STARTSCREEN;
        gameState = GameState.INGAME;
        activePlayer = players[0];
    }

    // Update is called once per frame
    void Update()
    {
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
                            drawled = false;
                            // Initialize movement
                            tilesToMoveThrough = board.Move(activePlayer, drawnCard);
                            currentTileIndex = 0;
                            moveTimer = 0f;
                            startPosition = activePlayer.currentTile.transform.position;
                            endPosition = tilesToMoveThrough[0].transform.position;
                            moving = true;
                        }
                        if (moving) //player movement
                        {
                            if (currentTileIndex < tilesToMoveThrough.Count)
                            {
                                moveTimer += Time.deltaTime;
                                float t = moveTimer / moveSpeed;
                                activePlayer.transform.position = Vector3.Lerp(startPosition, endPosition, t);
                                if (t >= 1f)
                                {
                                    activePlayer.currentTile = tilesToMoveThrough[currentTileIndex];
                                    currentTileIndex++;
                                    if (currentTileIndex < tilesToMoveThrough.Count)
                                    {
                                        startPosition = activePlayer.transform.position;
                                        endPosition = tilesToMoveThrough[currentTileIndex].transform.position;
                                        moveTimer = 0f;
                                    }
                                }
                            }
                            else
                            {
                                // Finalize movement
                                moving = false;
                                activePlayer.turn = false;
                                activePlayer.turnNumber++;
                                activePlayer.cam.Priority = 0;
                                TurnNumber.text = "" + activePlayer.turnNumber;
                                activePlayer.currentTile = tilesToMoveThrough[tilesToMoveThrough.Count - 1];
                                if (activePlayer.currentTile.end)
                                { 
                                    gameState = GameState.GAMEOVER;
                                }
                                tilesToMoveThrough.Clear();
                            }
                        }
                    }
                    else
                    {
                        activePlayer.skipTurn = false;
                        activePlayer.turn = false;
                        activePlayer.turnNumber++;
                        activePlayer.cam.Priority = 0;
                        TurnNumber.text = "" + activePlayer.turnNumber;
                    }
                }
                else 
                {
                    NextPlayer();
                }
                break;
            case GameState.GAMEOVER:
                Debug.Log("GameOver");
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
                    if (i < players.Count - 1)
                    {
                        activePlayer = players[i + 1];
                    }
                    else
                    {
                        activePlayer = players[0];
                    }
                activePlayer.turn = true;
                activePlayer.cam.Priority = 1;
                TurnNumber.text = "" + activePlayer.turnNumber;
                return;
            }
        }
    }

    public void onDrawPress()
    {
        if (!moving)
        {
            drawled = true;
        }
    }

    enum GameState
    { 
        STARTSCREEN,PLAYERSELECT,GAMESTART,INGAME,GAMEOVER
    }
}
