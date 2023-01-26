using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public sealed class GM : NetworkBehaviour
{
    public static GM Instance{get; private set;}
    
    [field:SyncObject]
    public SyncList<PlayerBoard> players {get;}= new SyncList<PlayerBoard>();
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>

    [field:SyncVar]
    [field:SerializeField]
    public bool canStart{get;private set;}

    [field:SyncVar]
 [field:SerializeField]
    public bool DidStart{get;private set;}
    
    [field:SerializeField]
    [field:SyncVar]
    public int Turn{get;private set;}

    private void Awake()
    {
        Instance=this;
        players.Clear();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        
          Debug.Log("Players number= "+players.Count+" in the game");
          if(!IsServer)return;
          canStart= (players.Count>1)&&(players.All(PlayerBoard=>PlayerBoard.Instance.isReady));
          Debug.Log($"Can Start: {canStart}");
    }
   [Server]
    public void StartGame()
    {
        foreach (PlayerBoard pl in players)
        {
            pl.StartGame();
        }
        DidStart=true;
        BeginTurn();
    }
 [Server]
    public void StopGame()
    {

        foreach (PlayerBoard pl in players)
        {
            pl.StopGame();
        }
        DidStart=false;
    }
    [Server]
    public void BeginTurn( )
    {
      foreach(PlayerBoard pl in players)
      {
          pl.BeginTurn();
      }
    }
    [Server]
    public void EndTurn()
    {
        Turn=(Turn+1)%players.Count;
        BeginTurn();
    }
}
