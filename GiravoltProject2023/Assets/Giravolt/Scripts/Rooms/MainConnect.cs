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

public class MainConnect : MonoBehaviourPunCallbacks, IPunObservable
{
    // Start is called before the first frame update
    public static Dictionary<Player, GameObject> dicOfPlayers = new Dictionary<Player, GameObject>();
    [SerializeField] public List<string> noUsePlayerList = new List<string>();
    public List<GameObject> noUseGameObjectList = new List<GameObject>();
    private GameObject spawnedPlayerPrefab;
    private PhotonView pView;
    public Player tmpPhotonPlayer;
    public GameObject tmpFakePlayer;
    public GameObject fakePlayerPrefab;
    private Player newPlayer;
    private int i;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        pView = GetComponent<PhotonView>();
        i = 0;
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
        tmpFakePlayer = Instantiate(fakePlayerPrefab, new Vector3(0, 0, 0), Quaternion.identity, spawnedPlayerPrefab.transform);
        pView.RPC("AddPlayerToList", RpcTarget.All);        
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
    #region IPunObservable implementation
    
    [PunRPC]
    public void AddPlayerToList()
    {
        i++;
        tmpPhotonPlayer.NickName = "Player " + i;
        dicOfPlayers.Add(tmpPhotonPlayer, tmpFakePlayer);
        noUsePlayerList.Add(tmpPhotonPlayer.NickName.ToString());   
        noUseGameObjectList.Add(tmpFakePlayer);
    }
    #endregion
}
