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
    private MainConnect mC;
    void Awake()
    {
        mC = FindObjectOfType<MainConnect>();
    }
    public void SetPlayerInfo(Player player)
    {
        Player = player;
        text.text = mC.userName;
    }
}
