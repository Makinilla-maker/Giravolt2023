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
public class TestRelay : MonoBehaviour
{
    public TextMeshProUGUI joinCodeText;
    private string ipv;
    int port;
    byte[] bytesArr;
    byte[] key;
    byte[] connectionData;
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
            ipv = allocation.RelayServer.IpV4;
            port = allocation.RelayServer.Port;
            bytesArr = allocation.AllocationIdBytes;
            key = allocation.Key;
            connectionData = allocation.ConnectionData;
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
                ipv,
                (ushort)port,
                bytesArr,
                key,
                connectionData,
                joinAllocation.HostConnectionData
            );
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
                ipv,
                (ushort)port,
                bytesArr,
                key,
                connectionData,
                joinAllocation.HostConnectionData
            );

            NetworkManager.Singleton.StartClient();
        }catch(RelayServiceException e)
        {
            Debug.Log(e);
        }
        
    }
}
