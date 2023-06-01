using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCode : MonoBehaviour
{
    public int id;
    public bool isAssassin;

    public GameObject localPlayer;
    private void Awake()
    {
        id = PhotonNetwork.LocalPlayer.ActorNumber;
    }

    public void BecomeAssassin()
    {
            isAssassin = true;
    }
}
