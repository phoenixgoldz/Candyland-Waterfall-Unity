using System.Collections.Generic;
using UnityEngine;

public class board : MonoBehaviour
{

    [SerializeField] List<tile> path = new List<tile>();



    public tile Move(playerPawn player, card card)
    {
        tile tile = new tile();
        if (card.color == TILE_TYPE.SPECIAL) 
        {
            tile =  findSpecial(card);
        }
        else if (card.doubleCard)
        { 
            tile = findDouble(card);
        }
        else
        {
            tile = find(card);
        }

        //update player locations

        return tile;


    }
    public tile find(card card)
    {
        foreach (tile tile in path)
        {
            if (card.color == tile.color)
            {
                    return tile;
            }
        }
        return null;
    }

    public tile findSpecial(card card)
    {
        foreach (tile tile in path)
        {
            if (card.special == tile.special)
            {
                return tile;
            }
        }
        return null;
    }
    public tile findDouble(card card)
    {
        bool db = false;
        foreach (tile tile in path)
        {
            if (card.color == tile.color)
            {
                if (db)
                {
                    return tile;
                }
                db = true;  
            }
        }
        return null;
    }
}

