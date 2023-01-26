using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public sealed class GameManager : NetworkBehaviour
{
    public static GameManager Instance{get; private set;}
    
    [SyncObject]
    public readonly SyncList<PlayerFPS> players = new SyncList<PlayerFPS>();
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>

    [SyncVar]
    public bool canStart;
    private void Awake()
    {
        Instance=this;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        
          Debug.Log("Players number= "+players.Count);
          if(!IsServer)return;
          canStart=players.All(PlayerFPS=>PlayerFPS.isReady);
          Debug.Log($"Can Start: {canStart}");
    }
   [Server]
    public void StartGame()
    {
        for(int i=0;i<players.Count;i++)
        {
            players[i].StartGame();
        }
    }
 [Server]
    public void StopGame()
    {
         for(int i=0;i<players.Count;i++)
        {
            players[i].StopGame();
        }
    }
}
