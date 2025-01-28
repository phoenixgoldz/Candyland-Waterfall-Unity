using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //color stuff
    [SerializeField] Material[] c_materials;

    //setup
    [SerializeField] public List<playerPawn> players;
    [SerializeField] public board board;
    GameState gameState;
    [SerializeField] CardManager cardManager;
    [SerializeField] tile startTile;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject DrawButton;
    
    //player updates
    playerPawn activePlayer;
    private bool drawled = false;
    [SerializeField] TMP_Text TurnNumber;
    [SerializeField] Image CardImg;

    //player movement
    private bool moving = false;
    List<tile> tilesToMoveThrough = new List<tile>();
    [SerializeField] float moveSpeed = 0.5f;
    private int currentTileIndex = 0;
    private float moveTimer = 0f;
    private Vector3 startPosition;
    private Vector3 endPosition;

    void Start() 
    {
        //tests
        //board.testFind();
        //board.testFindDouble();
        //board.testFindSpecial();

        // Initialize players
        players = new List<playerPawn>();
        for (int i = 0; i < PlayerPrefs.GetInt("players"); i++) {
            playerPrefab.GetComponentInChildren<Renderer>().material = c_materials[i];
            
            GameObject Go = Instantiate<GameObject>(playerPrefab, startTile.transform.position, Quaternion.identity);
            playerPawn player = Go.AddComponent<playerPawn>();
            player.cam = Go.GetComponentInChildren<CinemachineCamera>();
            player.currentTile = startTile;

            // Initialize turn numbers
            player.turnNumber = 1; // Set initial turn number to 1

            players.Add(player);
        }
        //gameState = GameState.STARTSCREEN;
        gameState = GameState.INGAME;

        // Set the first player as the active player
        activePlayer = players[0];
        activePlayer.turn = true; 

        // Display the initial turn number in the UI
        TurnNumber.text = "1"; // Start with turn 1
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
                            CardImg.sprite = drawnCard.cardImg;
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
                            DrawButton.SetActive(false);
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

                                if (activePlayer.currentTile.special == SPECIAL_TYPE.LICORICE)
                                {
                                    activePlayer.skipTurn = true;
                                }
                                if (activePlayer.currentTile.end)
                                { 
                                    gameState = GameState.GAMEOVER;
                                }
                                tilesToMoveThrough.Clear();
                                DrawButton.SetActive(true);
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
