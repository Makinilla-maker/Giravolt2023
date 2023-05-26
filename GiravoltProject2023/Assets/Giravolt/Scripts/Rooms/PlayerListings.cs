using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PlayerListings : MonoBehaviourPunCallbacks
{
    [SerializeField] private PlayerListing playerListing;
    [SerializeField] private Transform content;
    private List<PlayerListing> _listings = new List<PlayerListing>();
    private void Awake()
    {
        
    }
    public void AddPlayerListing(Player player)
    {
        PlayerListing listing = Instantiate(playerListing, content);
                if (listing != null)
                {
                    listing.SetPlayerInfo(player);
                    _listings.Add(listing);
                }
    }
    private void GetCurrentRoomPlayers()
    {
        foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
    }
    // public override void OnPlayerEnteredRoom(Player newPlayer)
    // {
    //     Debug.Log("THEEEEEEEEE PLAYEEEEEEEER ENTEEEEEEEREEEEEEEEEED AAAAA RROOOOOOOOOOOOOOOOM ATTACHING NAME TO THE UI");
    //     AddPlayerListing(newPlayer);
    // }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.myPlayer == otherPlayer);
                if(index != -1)
                {
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
    }
}
