using System.Net;
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

public class TestRelay : MonoBehaviour
{
    public TextMeshProUGUI joinCodeText;
    [SerializeField] private GameObject playerPrefabA; //add prefab in inspector
    [SerializeField] private GameObject playerPrefabB; //add prefab in inspector
    [SerializeField] private SpawnXRrigPlayer player;

    
    [ServerRpc(RequireOwnership=false)] //server owns this object but client can request a spawn
    public void SpawnPlayerServerRpc(ulong clientId,int prefabId)
    {
        GameObject newPlayer;
        if (prefabId==0)
        {
            newPlayer = player.SpawnPlayer();
        }
                
        else
        {
            newPlayer = player.SpawnPlayer();
        }
            
        NetworkObject netObj=newPlayer.GetComponent<NetworkObject>();
        newPlayer.SetActive(true);
        netObj.SpawnAsPlayerObject(clientId,true);
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
    

    void Awake()
    {
        joinCodeText = GameObject.Find("JoinCode").GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    private async void Start()
    {
        await UnityServices.InitializeAsync();
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed In" + AuthenticationService.Instance.PlayerId);
        };

        AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public async void CreateRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);
            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log(joinCode);
            joinCodeText.text = joinCode;

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
            );

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
            );
            NetworkManager.Singleton.StartHost();
        } catch(RelayServiceException e)
        {
            Debug.Log(e);
        }
    }
    public async void JoinRelay(string joinCode)
    {
        try
        {
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(
                joinAllocation.RelayServer.IpV4,
                (ushort)joinAllocation.RelayServer.Port,
                joinAllocation.AllocationIdBytes,
                joinAllocation.Key,
                joinAllocation.ConnectionData,
                joinAllocation.HostConnectionData
            );
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
                joinAllocation.RelayServer.IpV4,
                (ushort)joinAllocation.RelayServer.Port,
                joinAllocation.AllocationIdBytes,
                joinAllocation.Key,
                joinAllocation.ConnectionData,
                joinAllocation.HostConnectionData
            );

            NetworkManager.Singleton.StartClient();
        }catch(RelayServiceException e)
        {
            Debug.Log(e);
        }
    }
}
