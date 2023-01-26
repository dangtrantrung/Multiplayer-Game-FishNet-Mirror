using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FishNet;

public sealed class MultiplayView : View
{
    [SerializeField]
    private Button hostButton;

    [SerializeField]
    private Button connectButton;

    [SerializeField]
    private Button ExitButton;

    public override void Initialize()
    {
        hostButton.onClick.AddListener(()=>
        
        {
            InstanceFinder.ServerManager.StartConnection();
            InstanceFinder.ClientManager.StartConnection();
        });

        connectButton.onClick.AddListener(()=>{InstanceFinder.ClientManager.StartConnection();});
        ExitButton.onClick.AddListener(Application.Quit);
        base.Initialize();;
    }

    
}
