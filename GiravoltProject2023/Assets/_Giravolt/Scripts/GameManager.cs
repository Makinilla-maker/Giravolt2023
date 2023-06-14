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
using Autohand;

[System.Serializable]
public enum GameState
{
    INLOBBY,
    INGAME,
    INVOTATION,
    ENDGAME,
    NONE,
}
public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private GameObject player;
    private PhotonView pView;
    [SerializeField] private Transform lobbyPosition;
    private GameObject lobbyVotationObjects;
    private GameObject trackerOffset;
    public GameState gameState;
    private AutoHandPlayer playerBody;
    private void Awake()
    {
        gameState = GameState.INLOBBY;
        pView = GetComponent<PhotonView>();
        player = GameObject.Find("OculusPlayer");
        lobbyPosition = GameObject.Find("LobbyStartPosition").GetComponent<Transform>();
        trackerOffset = GameObject.Find("TrackerOffsets");
        lobbyVotationObjects = GameObject.Find("VotationObject");
        lobbyVotationObjects.SetActive(false);
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
        playerBody = GameObject.Find("Auto Hand Player").GetComponent<AutoHandPlayer>();
        // lock doors
        lobbyVotationObjects.SetActive(true);
        playerBody.SetPosition(lobbyPosition.position);
        UpdateGameState(GameState.INVOTATION);
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
