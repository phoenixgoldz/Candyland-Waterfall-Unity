using UnityEngine;

public class tile : MonoBehaviour
{
    [SerializeField] public TILE_TYPE color;
    [SerializeField] public SPECIAL_TYPE special;
    [SerializeField] public bool end = false;
    public int index;


}
