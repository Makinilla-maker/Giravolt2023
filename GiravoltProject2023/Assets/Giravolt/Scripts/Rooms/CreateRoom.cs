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
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
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
        PhotonNetwork.JoinOrCreateRoom(roomName.text, roomOption, TypedLobby.Default);
        
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("OLEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE SHA CREAR ROOM amb nom: " + roomName.text);
        StartCoroutine(WaitToLoadScene("SampleScene"));
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("No sha creat la room, noob");
    }
    IEnumerator WaitToLoadScene(string n)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(n);
    }
}
