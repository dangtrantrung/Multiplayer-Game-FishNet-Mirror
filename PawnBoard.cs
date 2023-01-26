using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet;
using System.Linq;
using DG.Tweening;

public class PawnBoard : NetworkBehaviour
{
    [SyncVar]
    public PlayerBoard controllingPlayerBoard;
     [SyncVar]
    public int currentPosition;
    [SerializeField]
    //private float movementSpeed;
    private bool _isMoving;
    
    // easy to cheat code by another app when use require ownership = false
     [ServerRpc(RequireOwnership =false)]

     public void ServerMove( int steps)
     {   if(_isMoving) return;
       _isMoving =true;

         Tile[] tiles=Board.Instance.Slice(currentPosition, currentPosition+steps);
         int controllingplayerIndex=GM.Instance.players.IndexOf(controllingPlayerBoard);
         Debug.Log("controllingplayerIndex:" + controllingplayerIndex);
         Vector3[] path=tiles.Select(tile=>tile.SpawnPawnPosition[controllingplayerIndex].position).ToArray();
        Tween tween=  transform.DOPath(path,1.25f);
        tween.OnComplete(()=>
        {_isMoving=false;
        currentPosition+=steps;
        GM.Instance.EndTurn();
     });
     tween.Play();
     }

    /*private IEnumerator Move(int steps)
   {
         Tile[] tiles=Board.Instance.Slice(currentPosition, currentPosition+steps);
     foreach(Tile tile in tiles)
     {
         if(transform.position!=tile.transform.position)
         {
             transform.position=Vector3.MoveTowards(transform.position,tile.transform.position,movementSpeed*Time.deltaTime);
         }
     }
     yield break;
   }*/
}
