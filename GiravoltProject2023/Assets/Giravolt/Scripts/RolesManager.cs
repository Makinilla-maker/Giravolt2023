using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon.StructWrapping;

public class RolesManager : MonoBehaviour
{
    private PhotonView pView;
    public int assassinID;
    public static List<GameObject> players = new List<GameObject>();

    public GameObject localPlayer;
    public bool isLocalAssassin;

    private void Awake()
    {
        pView = GetComponent<PhotonView>();
        assassinID = PlayerListings.globalAssassinID;
        Debug.Log("Assassin ID: " + assassinID);

        localPlayer = GameObject.Find("OculusPlayer");

        if (PhotonNetwork.IsMasterClient)
        {
            pView.RPC("SetAssassin", RpcTarget.All, assassinID);
        }

        [PunRPC]
        void SetAssassin(int assassinID)
        {
            if (PhotonNetwork.LocalPlayer == PhotonNetwork.PlayerList[assassinID])
            {
                localPlayer.GetComponent<PlayerCode>().BecomeAssassin();
            }
        }
    }
}
