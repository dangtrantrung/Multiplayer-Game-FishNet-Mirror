using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using TMPro;

public sealed class GManager : NetworkBehaviour
{
    public static GManager Instance{get; private set;}
        
    [field:SyncObject]
    public SyncList<PL> players {get;}= new SyncList<PL>();
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>

    [field:SyncVar]
    [field:SerializeField]
    public bool canStart{get;private set;}

    [field:SyncVar]
    [field:SerializeField]
    public bool DidStart{get;private set;}

    [field:SyncVar]
    public bool isReady
    {  get;
        [ServerRpc(RequireOwnership =false)]
        set;
    }
    [field:SyncVar]
    public int waitsecond
    {  
        get;
        [ServerRpc(RequireOwnership =false)]
        set;
    }
       [field:SyncVar]

    public int numberofplayers
 {  
        get;
        [ServerRpc(RequireOwnership =false)]
        set;
    }

     [field:SyncVar]

    public int numberofRounds
     {  
        get;
        [ServerRpc(RequireOwnership =false)]
        set;
    }
      [field:SyncVar]

    public int current_round
     {  
        get;
        [ServerRpc(RequireOwnership =false)]
        set;
    }
       

    public Timer Timer;


    

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
        
          Debug.Log("number of Players in the game: "+players.Count);
           Debug.Log("number of Players in the game settings: "+GManager.Instance.numberofplayers);


          if(!IsServer)return;
          
          if(GManager.Instance.numberofplayers==2)
          {
            canStart= (players.Count==2)&&(players.All(PL=>PL.Instance.isReady));
          }
         if(GManager.Instance.numberofplayers==3)
          {
            canStart= (players.Count==3)&&(players.All(PL=>PL.Instance.isReady));
          }
          Debug.Log($"Can Start Game: {canStart}");
    }
   [Server]
    public void StartGame()
    {
        GManager.Instance.current_round=1;
        
        foreach (PL pl in players)
        {
            pl.StartGame();
        }
        DidStart=true;
        
    }
 [Server]
    public void StopGame()
    {

        foreach (PL pl in players)
        {
            pl.StopGame();
        }
        DidStart=false;
    }
}
