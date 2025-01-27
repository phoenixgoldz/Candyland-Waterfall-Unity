using Unity.Cinemachine;
using UnityEngine;

public class playerPawn : MonoBehaviour
{

    [SerializeField] public Color color;
    [SerializeField] public tile currentTile;
    [SerializeField] public bool turn;
    [SerializeField] public bool skipTurn;
    [SerializeField] public int turnNumber;
    [SerializeField] public CinemachineCamera cam;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
