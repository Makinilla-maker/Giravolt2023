using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private PhotonView pView;
    
    private void Awake()
    {
        pView = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //ConnectToServer();
    }
    void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connect To Server...");
    }
    //public override void OnConnectedToMaster()
    //{
    //    base.OnConnectedToMaster();
    //    RoomOptions roomOption = new RoomOptions();
    //    roomOption.MaxPlayers = 5;
    //    roomOption.IsVisible = true;
    //    roomOption.IsOpen = true;
    //    roomOption.PublishUserId = true;
    //    PhotonNetwork.JoinOrCreateRoom(roomName, roomOption, TypedLobby.Default);
    //}
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a Room");
        base.OnJoinedRoom();
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("New player joined to the room");
        base.OnPlayerEnteredRoom(newPlayer);

    }
    
}
