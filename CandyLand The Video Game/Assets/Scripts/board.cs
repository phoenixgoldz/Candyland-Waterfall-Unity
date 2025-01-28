using System;
using System.Collections.Generic;
using UnityEngine;

public class board : MonoBehaviour
{

    [SerializeField] List<tile> path = new List<tile>();

    private void Start()
    {
    }

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
            tile =  findSpecial(card, path);
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

    public tile findSpecial(card card, List<tile> Path)
    {
        foreach (tile tile in Path)
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

    public bool testFindSpecial()
    {
        card card = new card();
        //random card color
        card.special = (SPECIAL_TYPE)UnityEngine.Random.Range(0, 5);
        card.color = TILE_TYPE.SPECIAL;
        Debug.Log("card to find");
        Debug.Log(card.special);
        List<tile> Path = new List<tile>();
        //random tiles
        for (int i = 0; i < 20; i++)
        {
            tile t = new tile();
            t.color = (TILE_TYPE)UnityEngine.Random.Range(0, 5);
            t.special = SPECIAL_TYPE.NONE;
            Path.Add(t);
        }
        //insert special randomly inside path
        tile ti = new tile();
        ti.special = card.special;
        Path.Insert(UnityEngine.Random.Range(0, 20), ti);


        //run method
        tile found = findSpecial(card, Path);

        //check result
        Debug.Log("found special: " + found.special);

        //list of tiles
        Debug.Log("tiles list");
        Debug.Log("=============");
        for (int i = 0; i < Path.Count - 1; i++)
        {
            Debug.Log("#" + i + " Tile Color: " + Path[i].color);
            Debug.Log("#" + i + " Tile Special: " + Path[i].special);
        }
        return (found.special == card.special);
    }


    public bool testFind()
    { 
        card card = new card();
        //random card color
        card.color = (TILE_TYPE)UnityEngine.Random.Range(0, 5);
        Debug.Log("card to find");
        Debug.Log(card.color);
        List<tile> Path = new List<tile>();
        //random tiles
        for (int i = 0; i < 20; i++)
        { 
            tile t = new tile();  
            t.color = (TILE_TYPE)UnityEngine.Random.Range(0, 5);
            Path.Add(t);
        }
        //end of edge case
        tile ti = new tile();
        ti.color = card.color;
        Path.Add(ti);

        //run method
        tile found = find(card, Path);

        //check result
        Debug.Log("found color: " + found.color);

        //list of tiles
        Debug.Log("tiles list");
        Debug.Log("=============");
        for (int i = 0; i < Path.Count-1; i++)
        { 
            Debug.Log("#" + i + " Tile Color: " + Path[i].color);
        }
        return (found.color == card.color);
    }

    public bool testFindDouble()
    {
        card card = new card();
        //random card color
        card.color = (TILE_TYPE)UnityEngine.Random.Range(0, 5);
        Debug.Log("card to find");
        Debug.Log(card.color);
        List<tile> Path = new List<tile>();
        //random tiles
        for (int i = 0; i < 10; i++)
        {
            tile t = new tile();
            t.color = (TILE_TYPE)UnityEngine.Random.Range(0, 5);
            Path.Add(t);
        }
        //end of edge case
        tile ti = new tile();
        ti.color = card.color;
        Path.Add(ti);

        //run method
        tile found = findDouble(card, Path);

        //check result
        Debug.Log("found color: " + found.color);

        //list of tiles
        Debug.Log("tiles list");
        Debug.Log("=============");
        for (int i = 0; i < Path.Count - 1; i++)
        {
            Debug.Log("#" + i + " Tile Color: " + Path[i].color);
        }
        return (found.color == card.color);
    }
}

