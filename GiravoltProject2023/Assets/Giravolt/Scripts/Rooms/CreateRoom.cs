using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI roomName;
    [SerializeField] private PlayerListings playerListings; 
    private string name;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        playerListings = FindObjectOfType<PlayerListings>();

    }
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void GiraCreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;

        name = roomName.text;
        OnConnectedToMaster();        
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        RoomOptions roomOption = new RoomOptions();
        roomOption.MaxPlayers = 5;
        roomOption.IsVisible = true;
        roomOption.IsOpen = true;
        roomOption.PublishUserId = true;
        PhotonNetwork.JoinOrCreateRoom(name, roomOption, TypedLobby.Default);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("OLEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE SHA CREAR ROOM amb nom: " + roomName.text);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("No sha creat la room, noob");
    }
    public void LoadSampleScene()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            SceneManager.LoadScene("SampleScene");
            //hotonNetwork.LoadLevel("SampleScene");
        }        
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("THEEEEEEEEE PLAYEEEEEEEER ENTEEEEEEEREEEEEEEEEED AAAAA RROOOOOOOOOOOOOOOOM ATTACHING NAME TO THE UI");
        playerListings.AddPlayerListing(newPlayer);
    }
    
}
