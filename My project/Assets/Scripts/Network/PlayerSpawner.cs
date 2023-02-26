using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Oculus.Platform.Samples.VrHoops;
using Unity.Netcode.Components;
public class PlayerSpawner : NetworkBehaviour
{
    private TestRelay relay;
    public SpawnXRrigPlayer player;
    void Awake()
    {
        relay = GameObject.Find("TestRelay").GetComponent<TestRelay>();
    }
    public override void OnNetworkSpawn()
    {
    if (IsServer)
    {
        relay.SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId,0);
        player.xRigGo.AddComponent<NetworkObject>();
        player.xRigGo.AddComponent<NetworkTransform>();
        player.mainCameraGo.AddComponent<NetworkObject>();
        player.mainCameraGo.AddComponent<NetworkTransform>();
        player.rightControllerGo.AddComponent<NetworkObject>();
        player.rightControllerGo.AddComponent<NetworkTransform>();
        player.xrControllerRightGo.AddComponent<NetworkObject>();
        player.xrControllerRightGo.AddComponent<NetworkTransform>();
        player.leftControllerGo.AddComponent<NetworkObject>();
        player.leftControllerGo.AddComponent<NetworkTransform>();
        player.xrControllerLeftGo.AddComponent<NetworkObject>();
        player.xrControllerLeftGo.AddComponent<NetworkTransform>();
        
        
    }
        
    else
        relay.SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId,1);
        player.xRigGo.AddComponent<NetworkObject>();
        player.xRigGo.AddComponent<NetworkTransform>();
        player.mainCameraGo.AddComponent<NetworkObject>();
        player.mainCameraGo.AddComponent<NetworkTransform>();
        player.rightControllerGo.AddComponent<NetworkObject>();
        player.rightControllerGo.AddComponent<NetworkTransform>();
        player.xrControllerRightGo.AddComponent<NetworkObject>();
        player.xrControllerRightGo.AddComponent<NetworkTransform>();
        player.leftControllerGo.AddComponent<NetworkObject>();
        player.leftControllerGo.AddComponent<NetworkTransform>();
        player.xrControllerLeftGo.AddComponent<NetworkObject>();
        player.xrControllerLeftGo.AddComponent<NetworkTransform>();
    }
    private void OnConnectedToServer()
    {
        relay.SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId,1);
    }
}

