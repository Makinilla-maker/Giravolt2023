using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon.StructWrapping;

public class RolesManager : MonoBehaviour
{
    public int assassinID;
    public List<Player> players;

    private void Awake()
    {
        assassinID = PlayerListings.assassinID;
        Debug.Log("Assassin ID: " + assassinID);
        players = PlayerListings.players;
        //PhotonNetwork.CurrentRoom.GetPlayer(assassinID);
    }

    private void SetAssassin(int assassinID)
    {
        
    }
}
