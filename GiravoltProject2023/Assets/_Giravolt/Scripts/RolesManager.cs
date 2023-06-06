using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon.StructWrapping;

public class RolesManager : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] bool imAssassin;

    private void Awake()
    {
        id = PhotonNetwork.LocalPlayer.ActorNumber;
        imAssassin = false; 
        SetLocalPlayerAsAssassin();
    }

    public void SetLocalPlayerAsAssassin()
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == (int)PhotonNetwork.LocalPlayer.CustomProperties["AssassinID"])
        {
            imAssassin = true;
            Debug.Log("I am the assassin");
        }
        else
        {
            imAssassin= false;
        }
    }
}
