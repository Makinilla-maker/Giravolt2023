using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PlayerListings : MonoBehaviourPunCallbacks
{
    [SerializeField] private PlayerListing _playerListing;
    [SerializeField] private Transform content;
    private List<PlayerListing> _listings = new List<PlayerListing>();
    private void Awake()
    {
        
    }
    public override void OnEnable()
    {
        base.OnEnable();
        GetCurrentRoomPlayers();
    }
    public override void OnDisable()
    {
        base.OnDisable();
        for(int i = 0; i < _listings.Count; ++i)
        {
            Destroy(_listings[i].gameObject);
        }
        _listings.Clear();
    }
    public void AddPlayerListing(Player player)
    {
        int index = _listings.FindIndex(x => x.Player == player);
        if(index != -1)
        {
            _listings[index].SetPlayerInfo(player);
        }
        else
        {
            PlayerListing listing = Instantiate(_playerListing, content);
            if (listing != null)
            {
                listing.SetPlayerInfo(player);
                _listings.Add(listing);
            }
        }
        
    }
    public void GetCurrentRoomPlayers()
    {
        int i = 0;        
        foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            playerInfo.Value.NickName = "Player " + PhotonNetwork.CurrentRoom.Players.Count;
            i++; 
            AddPlayerListing(playerInfo.Value);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if(index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }
}
