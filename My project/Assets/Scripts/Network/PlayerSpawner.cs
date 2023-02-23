using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;
public class PlayerSpawner : NetworkBehaviour
{
    private TestRelay relay;
    void Awake()
    {
        relay = GameObject.Find("TestRelay").GetComponent<TestRelay>();
    }
    public override void OnNetworkSpawn()
    {
    if (IsServer)
        relay.SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId,0);
    else
        relay.SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId,1);
    }
    private void OnConnectedToServer()
    {
        relay.SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId,1);
    }
}

