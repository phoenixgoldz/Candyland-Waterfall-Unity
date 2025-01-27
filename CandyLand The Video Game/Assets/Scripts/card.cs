using UnityEngine;

public class card : MonoBehaviour
{
    [SerializeField] public TILE_TYPE color;
    [SerializeField] public SPECIAL_TYPE special;
    [SerializeField] public bool doubleCard;

    [SerializeField] public Sprite cardImg;
}
