using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class MainViewBoard :View
{
    [SerializeField]
    private Button moveforwardButton;

    [SerializeField]
    private Button moveBackwardButton;

    public override void Initialize()
    {
        moveforwardButton.onClick.AddListener(()=>PlayerBoard.Instance.controlledPawn.ServerMove(1));
        moveBackwardButton.onClick.AddListener(()=>PlayerBoard.Instance.controlledPawn.ServerMove(-1));
     base.Initialize();
    }
}
