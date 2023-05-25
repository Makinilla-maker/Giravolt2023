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
    private Transform lobbyPosition;
    [SerializeField] GameState gameState;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        gameState = GameState.INLOBBY;
        pView = GetComponent<PhotonView>();
        player = GameObject.Find("OculusPlayer");
        lobbyPosition = GetComponentInChildren<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
