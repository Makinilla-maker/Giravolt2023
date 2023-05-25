using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
public class RoomListing : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    public RoomInfo _roomInfo { get; private set; }
    public void SetRoomInfo(RoomInfo roomInfo)
    {
        _roomInfo = roomInfo;
        text.text = roomInfo.MaxPlayers + ", " + roomInfo.Name;
    }
    
}
