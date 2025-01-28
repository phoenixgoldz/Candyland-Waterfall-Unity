using System;
using System.Collections.Generic;
using UnityEngine;

public class board : MonoBehaviour
{

    [SerializeField] List<tile> path = new List<tile>();

    public List<tile> Move(playerPawn player, card card)
    {
        tile tile = new tile();
        List<tile> remainingPath = new List<tile>();
        bool afterPlayerTile = false;
        foreach (tile t in path)
        {
            if (afterPlayerTile)
            {
                remainingPath.Add(t);
            }
            if (player.currentTile == t)
            {
                afterPlayerTile = true;
            }
        }


        if (card.color == TILE_TYPE.SPECIAL) 
        {
            Debug.Log("special");
            tile =  findSpecial(card);
        }
        else if (card.doubleCard)
        {
            Debug.Log("double");
            tile = findDouble(card, remainingPath);
        }
        else
        {
            Debug.Log("normal");
            tile = find(card, remainingPath);
        }

        //update player locations
        List<tile> betweenPath = new List<tile>();
        afterPlayerTile = false;
        int pidx = 0;
        int tidx = 0;
        int i = 0;
        foreach (tile t in path)
        {
            if (player.currentTile == t)
            {
                pidx = i;
                afterPlayerTile = true;
            }
            if (afterPlayerTile)
            {
                betweenPath.Add(t);
            }
            if (t == tile)
            {
                tidx = i;
                break;
            }
            i++;
        }
        //if 
        if (betweenPath.Count == 0)
        {
            betweenPath = new List<tile>();
            bool afterSpecialTile = false;
            foreach (tile t in path)
            {
                if (tile == t)
                {
                    afterSpecialTile = true;
                }
                if (afterSpecialTile)
                {
                    betweenPath.Insert(0,t);
                }
                if (t == player.currentTile)
                {
                    break;
                }
            }
        }


        return betweenPath;


    }
    public tile find(card card, List<tile> remainingPlayerPath)
    {
        foreach (tile tile in remainingPlayerPath)
        {
            if (tile.end) return tile;
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
            if (tile.end) return tile;
            if (card.special == tile.special)
            {
                return tile;
            }
        }
        return null;
    }
    public tile findDouble(card card, List<tile> remainingPlayerPath)
    {
        bool db = false;
        foreach (tile tile in remainingPlayerPath)
        {
            if (tile.end) return tile;
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

