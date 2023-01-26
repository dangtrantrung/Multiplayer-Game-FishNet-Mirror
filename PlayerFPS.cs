using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;
using FishNet.Object.Synchronizing;
using UnityEngine.AddressableAssets;
using FishNet.Connection;

public sealed class PlayerFPS : NetworkBehaviour
{

    public static PlayerFPS Instance{get; private set;}
    [SyncVar]
    public string username;
    [SyncVar]
    public Pawn controlledPawn;
   
   
   /* [field: SyncVar]
    public bool isReady 
    {get; 
    [ServerRpc]
    private set;}*/

    [SyncVar]
    public bool isReady;

    public override void OnStartServer()
    {
        base.OnStartServer();
        GameManager.Instance.players.Add(this);
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        if(!IsOwner)return;
        Instance=this;
        UIManager.Instance.Initialize();
        UIManager.Instance.Show<LobbyView>();
    }
    public override void OnStopServer()
    {
        base.OnStopServer();

        GameManager.Instance.players.Remove(this);
    }


    [ServerRpc(RequireOwnership =false)]
    public void ServerSetReady(bool value)
    {
        isReady=value;
    }


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if(!IsOwner)return;
        if(Input.GetKeyDown(KeyCode.R))
        {
            ServerSetReady(!isReady);
        }
        /*if(Input.GetKeyDown(KeyCode.P))
        {
            ServerSpawnPawn();
        }*/
    }

   

    public void StartGame()
    {
         GameObject pawnPrefab=Addressables.LoadAssetAsync<GameObject>("Pawn").WaitForCompletion();
         GameObject pawninstance=Instantiate(pawnPrefab);
         Spawn(pawninstance,Owner);
         controlledPawn=pawninstance.GetComponent<Pawn>();
         controlledPawn.controllingPlayer=this;
         TargerPawnSpawned(Owner);
    }
 [ServerRpc(RequireOwnership =false)]
 public void ServerSpawnPawn()
 {
     StartGame();
 }

    [TargetRpc]
    public void TargerPawnSpawned(NetworkConnection connection)
    {
        UIManager.Instance.Show<MainViewFPS>();    
    }

     public void StopGame()
    {
         if(controlledPawn!=null&&controlledPawn.IsSpawned)
         {
             controlledPawn.Despawn();
         }
    }

    [TargetRpc]

    public void TargetPawnKilled(NetworkConnection connection)
    {
        UIManager.Instance.Show<RespawnView>();
    }
}
