using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Tile : MonoBehaviour
{
    [field: SerializeField]
    public Transform[] SpawnPawnPosition {get; private set;}
}
