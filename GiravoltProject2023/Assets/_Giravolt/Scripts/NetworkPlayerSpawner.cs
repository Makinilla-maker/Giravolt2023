using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject spawnedPlayerPrefab;
    public Transform spawnPoint;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("This is the joined room of the network player spawner");
        
        base.OnJoinedRoom();
        //spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", spawnPoint.position, Quaternion.identity);
    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
