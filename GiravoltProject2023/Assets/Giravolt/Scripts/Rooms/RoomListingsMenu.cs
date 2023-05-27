using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] RoomListing roomListing;
    [SerializeField] Transform content;
    private List<RoomListing> _listings = new List<RoomListing>();
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            // remove from list
            if(info.RemovedFromList)
            {
                int index = _listings.FindIndex(x => x._roomInfo.Name== info.Name);
                if(index != -1)
                {
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
            }
            // add to list
            else
            {
                RoomListing listing = Instantiate(roomListing, content);
                if (listing != null)
                {
                    listing.SetRoomInfo(info);
                    _listings.Add(listing);
                }
            }
            
        }
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
    }
}
