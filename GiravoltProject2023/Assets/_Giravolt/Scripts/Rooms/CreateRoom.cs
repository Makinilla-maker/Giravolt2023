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
    private string name;
    private GameObject playerListGameObject;
    private MainConnect mC;
    [SerializeField] private RolesManager rolesManager;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        mC = FindObjectOfType<MainConnect>();
    }
    private void Start()
    {
        playerListGameObject = GameObject.Find("PlayerListings");
        //playerListGameObject.SetActive(false);
        rolesManager = FindObjectOfType<RolesManager>();
        PhotonNetwork.ConnectUsingSettings();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            GiraCreateRoom();
        }
    }
    public void GiraCreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;

           
        RoomOptions roomOption = new RoomOptions();
        roomOption.MaxPlayers = 5;
        roomOption.IsVisible = true;
        roomOption.IsOpen = true;
        roomOption.PublishUserId = true;
        name = roomName.text;
        PhotonNetwork.JoinOrCreateRoom(name, roomOption, TypedLobby.Default);
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("New room with name: " + roomName.text);
        //this.gameObject.SetActive(false);
        //playerListGameObject.SetActive(true);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("No sha creat la room, noob");
    }
    public void LoadSampleScene()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            //mC.AssignRoles();
            mC.SetCustomNumber();
            rolesManager.SetLocalPlayerAsAssassin();
            StartCoroutine(WaitToLoadCorrectScene());
            
        }        
    }
    IEnumerator WaitToLoadCorrectScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("SampleScene");
    }
    public override void OnJoinedRoom()
    {
        //this.gameObject.SetActive(false);
        //playerListGameObject.SetActive(true);
    }
    
}
