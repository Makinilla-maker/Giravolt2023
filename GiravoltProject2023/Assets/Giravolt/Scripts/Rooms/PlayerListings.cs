using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PlayerListings : MonoBehaviourPunCallbacks
{
    [SerializeField] PlayerListing playerListing;
    [SerializeField] Transform content;
    private List<PlayerListing> _listings = new List<PlayerListing>();
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
                PlayerListing listing = Instantiate(playerListing, content);
                if (listing != null)
                {
                    listing.SetPlayerInfo(newPlayer);
                    _listings.Add(listing);
                }
    }
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
