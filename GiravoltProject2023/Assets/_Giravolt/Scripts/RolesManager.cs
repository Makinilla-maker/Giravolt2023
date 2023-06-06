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
    private void Start()
    {
        id = PhotonNetwork.LocalPlayer.ActorNumber;
        Debug.Log("THIS IS THE ID OF THE ASSASSIN: " + (int)PhotonNetwork.LocalPlayer.CustomProperties["AssassinID"]);
        imAssassin = false; 
        SetLocalPlayerAsAssassin();
    }

    public void SetLocalPlayerAsAssassin()
    {
        Debug.Log("FUNCTION BEING CALLED IN START");
        if (PhotonNetwork.LocalPlayer.ActorNumber == (int)PhotonNetwork.LocalPlayer.CustomProperties["AssassinID"])
        {
            imAssassin = true;
            Debug.Log("I AM the ASSASSIN");
        }
        else
        {
            imAssassin= false;
            Debug.Log("I am NOT the ASSASSIN");
        }
    }
}
