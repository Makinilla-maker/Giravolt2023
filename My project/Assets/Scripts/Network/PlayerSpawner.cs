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
    public SpawnXRrigPlayer player;
    public override void OnNetworkSpawn()
    {
        player.SpawnPlayer();
    }
}

