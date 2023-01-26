using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using System;

public class Board : NetworkBehaviour
{

    public static Board Instance {get; private set;}
    [field:SerializeField]
    public Tile[] tiles{get; private set;}
    public Tile tilepos;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        Instance=this;
       // DontDestroyOnLoad(this);
    }

    public int Wrap(int index)
    {
        return index<0?Math.Abs((tiles.Length-Math.Abs(index))%tiles.Length):index%tiles.Length;
    }

    public Tile[] Slice(int start, int end)
    {
        if(tiles.Length==0)return Array.Empty<Tile>();
        List<Tile>tileSlice=new List<Tile>();

        int steps =Math.Abs(end-start);
        if(end>start)
        {
            for(int i=start;i<=start+steps;i++)
            {
                tileSlice.Add( tiles[Wrap(i)]);
            }
        }
        else
        {
            for(int i=start;i>=start-steps;i--)
            {
               tileSlice.Add( tiles[Wrap(i)]);
            }
        }
        return tileSlice.ToArray();
    }
}
