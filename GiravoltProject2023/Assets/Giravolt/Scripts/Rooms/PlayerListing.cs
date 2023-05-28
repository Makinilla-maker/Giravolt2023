using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
public class PlayerListing : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    public Player Player {get; private set;}
<<<<<<< HEAD
    void Awake()
    {
        
    }
    public void SetPlayerInfo(Player player)
    {
        Player = player;
=======
    public void SetPlayerInfo(Player player)
    {
        Player = player;
        text.text = player.NickName;
>>>>>>> parent of 25d26528 (set custom name, must test online)
    }
}
