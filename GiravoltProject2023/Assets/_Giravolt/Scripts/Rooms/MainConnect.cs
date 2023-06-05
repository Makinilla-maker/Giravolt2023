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
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;
//using Oculus.Platform.Samples.VrHoops;

public class MainConnect : MonoBehaviourPunCallbacks, IPunObservable
{
    // Start is called before the first frame update
    public List<Player> ListOfPhotonPlayers = new List<Player>();
    [SerializeField] private List<string> PlayerList = new List<string>();
    private GameObject spawnedPlayerPrefab;
    private PhotonView pView;
    public Player tmpPhotonPlayer;
    public GameObject tmpFakePlayer;
    private Player newPlayer;
    private bool sendRoleInformation;
    public GameObject OculusPlayer;
    private int rnd;
    public int i;
    private bool doOnce;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        pView = GetComponent<PhotonView>();
        i = 0;
        sendRoleInformation = false;
        doOnce = false;
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
    }
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "SampleScene" && !doOnce)
        {
            OculusPlayer = GameObject.Find("OculusPlayer");
            doOnce = true;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(sendRoleInformation)
        {
            if(pView.IsMine)
            {
                for(int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; ++i)
                {
                    //stream.SendNext(ListOfPhotonPlayers[i].name);
                    //stream.SendNext(ListOfPhotonPlayers[i].isAssassin);
                }
            }
            else
            {
                for(int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; ++i)
                {
                    string name = (string)stream.ReceiveNext();
                    bool b = (bool)stream.ReceiveNext();
                    switch (name)
                    {
                        case "Player 1(Clone)":
                            OculusPlayer.GetComponent<PlayerCode>().isAssassin = b;
                            break;
                        case "Player 2(Clone)":
                            OculusPlayer.GetComponent<PlayerCode>().isAssassin = b;
                        break;
                        case "Player 3(Clone)":
                            OculusPlayer.GetComponent<PlayerCode>().isAssassin = b;
                            break;
                        case "Player 4(Clone)":
                            OculusPlayer.GetComponent<PlayerCode>().isAssassin = b;
                        break;
                        case "Player 5(Clone)":
                            OculusPlayer.GetComponent<PlayerCode>().isAssassin = b;
                        break;
                        default:
                        Debug.Log("Errors while sending the bool isAssassin to all players, did not get the name of the player right");
                        break;
                    }
                }
            }
        }
        
    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
    #region IPunObservable implementation
    public void CleanListOfPhotonPlayers()
    {
        foreach (Player p in ListOfPhotonPlayers)
        {
            if (p != null)
            {
                ListOfPhotonPlayers.Remove(p);
            }
        }
    }
    public void AddPlayerToList(Player p)
    {

        ListOfPhotonPlayers.Add(p);
    }
    public void AssignRoles()
    {
        rnd = Random.Range(0, PhotonNetwork.CurrentRoom.PlayerCount);
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; ++i)
        {
            bool isAssassin = (bool)ListOfPhotonPlayers[i].CustomProperties["isAssassin"];

            if (i == rnd)
            {
                Debug.Log("The assassin is: " + ListOfPhotonPlayers[i].NickName);
                isAssassin = true;
            }
            else
            {
                isAssassin = false;
            }
            

            Hashtable hash = new Hashtable();
            hash.Add("isAssassin", isAssassin);
            ListOfPhotonPlayers[i].SetCustomProperties(hash);
        }
        sendRoleInformation = true;
    }
    #endregion
}
