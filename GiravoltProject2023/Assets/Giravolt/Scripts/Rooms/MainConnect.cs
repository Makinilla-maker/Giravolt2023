using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class MainConnect : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
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
        Debug.Log("New player joined to the room");
        base.OnPlayerEnteredRoom(newPlayer);

    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("DIOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOS JOINED ROOM CORRECTLY");
    }
}
