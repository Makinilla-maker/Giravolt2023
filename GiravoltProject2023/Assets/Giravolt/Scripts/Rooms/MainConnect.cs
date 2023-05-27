using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class MainConnect : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private GameObject spawnedPlayerPrefab;
    [SerializeField] private PlayerListings playerListings;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        playerListings = FindObjectOfType<PlayerListings>();
    }
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("COnnecting to photon ___ ", this);
    }
    public override void OnConnectedToMaster()
    {
        if(!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("=============================== JOINED LOBBY ===============================");
        playerListings.GetCurrentRoomPlayers();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("POOOOOOOOOOOOOOOOOOOOOOLLAAAAAAAAAAAAAAAAAAAAAA");
        base.OnPlayerEnteredRoom(newPlayer);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", new Vector3(0,0,0), Quaternion.identity);
    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}
