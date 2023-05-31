using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using Photon;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine.Serialization;
using System.IO;

public class MainConnect : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public List<GameObject> ListsOfPlayers = new List<GameObject>();
    private GameObject spawnedPlayerPrefab;
    private PhotonView pView;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        pView = GetComponent<PhotonView>();
    }
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
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
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", new Vector3(0,0,0), Quaternion.identity);
        pView.RPC("AddPlayerToList", RpcTarget.All, spawnedPlayerPrefab);        
    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
    #region IPunObservable implementation
    [PunRPC]
    public void AddPlayerToList(GameObject player)
    {
        ListsOfPlayers.Add(player);
    }
    #endregion
}
