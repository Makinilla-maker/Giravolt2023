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

[System.Serializable]
public enum GameState
{
    INLOBBY,
    INGAME,
    ENDGAME,
    NONE,
}
[System.Serializable]
public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    private PhotonView pView;
    [SerializeField] GameState gameState;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        gameState = GameState.INLOBBY;
        pView = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
    public void UpdateGameState(GameState _gameState)
    {
        gameState = _gameState;
    }
    public void PlayerRoomController()
    {
        //PhotonNetwork.Room
    }
}
