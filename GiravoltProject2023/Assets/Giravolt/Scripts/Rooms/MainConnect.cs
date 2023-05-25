using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class MainConnect : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private GameObject spawnedPlayerPrefab;
    public Transform spawnPoint;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        Debug.Log("COnnecting to photon ___ ", this);
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("POOOOOOOOOOOOOOOOOOOOOOLLAAAAAAAAAAAAAAAAAAAAAA");
        base.OnPlayerEnteredRoom(newPlayer);

    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", spawnPoint.position, Quaternion.identity);
    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
