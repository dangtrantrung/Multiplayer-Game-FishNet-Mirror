using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using FishNet.Object.Synchronizing;
using FishNet.Connection;
using FishNet;



public sealed class PL : NetworkBehaviour
{
   public static PL Instance{get; private set;}
    [SyncVar]
    public string username;
    

    [field:SyncVar]
    public bool isReady
    {  get;
        [ServerRpc(RequireOwnership =false)]
        set;
    }
         
    public override void OnStartServer()
    {
        base.OnStartServer();
        GManager.Instance.players.Add(this);
        if(InstanceFinder.IsServer)
    {
        GManager.Instance.numberofplayers=PlayerPrefs.GetInt("numberofPlayer");
        GManager.Instance.waitsecond=PlayerPrefs.GetInt("WaitSeconds");
        GManager.Instance.numberofRounds=PlayerPrefs.GetInt("numofRounds");
    }
      
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        if(!IsOwner)return;
        Instance=this;
       ViewManager.Instance.Initialize();
       PL.Instance.username=PlayerPrefs.GetString("playername");
    }
    public override void OnStopServer()
    {
        base.OnStopServer();

        GManager.Instance.players.Remove(this);
    }

  

   [Server]

   public void StartGame()

   {  //int playerindex= GM.Instance.players.IndexOf(this);
      // Debug.Log ("player index: " + playerindex);
     TargetStartGame(Owner); 
     
        // SET TIMER
       GManager.Instance.Timer.gameObject.SetActive(true);
       Timer timer=GManager.Instance.Timer.GetComponent<Timer>();
       timer.Numsec=(double)GManager.Instance.waitsecond;

   }
   [Server]

   public void StopGame()

   {
      
   }

   [TargetRpc]
   public void  TargetStartGame(NetworkConnection networkConnection)
   {
       ViewManager.Instance.Show<MainViewP>();
      
       //Timer timer= new Timer(GManager.Instance.waitsecond);
       
   }
}
