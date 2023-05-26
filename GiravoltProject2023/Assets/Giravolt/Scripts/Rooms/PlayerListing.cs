using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
public class PlayerListing : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private Player myPlayer;
    public void SetPlayerInfo(Player player)
    {
        myPlayer = player;
        text.text = player.NickName;
    }
}
