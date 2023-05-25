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
using System.Runtime.CompilerServices;

[System.Serializable]
public enum GameState
{
    INLOBBY,
    INGAME,
    INVOTATION,
    ENDGAME,
    NONE,
}
[System.Serializable]
public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    private GameObject player;
    private PhotonView pView;
    [SerializeField] private Transform lobbyPosition;
    public GameState gameState;
    private void Awake()
    {
        gameState = GameState.INLOBBY;
        pView = GetComponent<PhotonView>();
        player = GameObject.Find("OculusPlayer");
        lobbyPosition = GameObject.Find("LobbyStartPosition").GetComponent<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnVotationBegin()
    {        
        pView.RPC("StartVotation", RpcTarget.All);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // send the info of the generated tasks 

        if (pView.IsMine)
        {
            
        }
        else
        {
            
        }
    }
    [PunRPC]
    public void StartVotation()
    {
        player.transform.position = lobbyPosition.position;
        gameState = GameState.INVOTATION;
    }
    [PunRPC]
    public void EndVotaion()
    {
        // unlock doors
        UpdateGameState(GameState.INGAME);
    }
    #endregion
    public void UpdateGameState(GameState _gameState)
    {
        gameState = _gameState;
    }
    public void PlayerRoomController()
    {
        //PhotonNetwork.Room
    }
}
