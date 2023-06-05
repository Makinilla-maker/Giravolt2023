using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.InputSystem;
using ExitGames.Client.Photon;


public class PlayerListings : MonoBehaviourPunCallbacks
{
    [SerializeField] private PlayerListing _playerListing;
    [SerializeField] private Transform content;
    private List<PlayerListing> _listings = new List<PlayerListing>();
    private int i;
    private MainConnect mC;
    public TextMeshProUGUI playerName;

    // Impostor selector
    public static int assassinID;
    public static List<Player> players = new List<Player>();

    private void Awake()
    {
        i = 1;
        mC = FindObjectOfType<MainConnect>();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        GetCurrentRoomPlayers();
    }
    public override void OnDisable()
    {
        base.OnDisable();
        for (int i = 0; i < _listings.Count; ++i)
        {
            Destroy(_listings[i].gameObject);
        }
        _listings.Clear();
    }
    public void AddPlayerListing(Player player)
    {
        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            _listings[index].SetPlayerInfo(player);
            bool isAssassin = (bool)player.CustomProperties["MePolla"];
            mC.AddPlayerToList(player);
        }
        else
        {
            PlayerListing listing = Instantiate(_playerListing, content);
            if (listing != null)
            {
                if(playerName.text != "")
                    player.NickName = playerName.text;
                else
                    player.NickName = "Player " + i;

                mC.tmpPhotonPlayer = player;
                listing.SetPlayerInfo(player);
                _listings.Add(listing);
                players.Add(player);
                mC.AddPlayerToList(player);
                bool isAssassin = (bool)player.CustomProperties["MePolla"];
            }
        }
    }
    public void GetCurrentRoomPlayers()
    {
        //if (PhotonNetwork.CurrentRoom.PlayerCount != 0)
        //{
        //    mC.CleanListOfPhotonPlayers();
        //}
            
            //if(PhotonNetwork.CurrentRoom.PlayerCount >= PhotonNetwork.CurrentRoom.MaxPlayers)
            //{
            //    PickAssassin();
            //}
            
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
            
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GetCurrentRoomPlayers();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
            mC.ListOfPhotonPlayers.Remove(otherPlayer);
            i--;
        }
    }

    void PickAssassin()
    {
        assassinID = Random.Range(0, PhotonNetwork.CurrentRoom.PlayerCount);
        Debug.Log("Assassin is the Player: " + assassinID);
    }
}
