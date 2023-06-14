using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine.SceneManagement;
public class RolesManager : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] bool imAssassin;
    private MainConnect mC;
    private bool doOnce = false;
    private void Start()
    {
            mC = GameObject.Find("MainNetworkGameObject").GetComponent<MainConnect>();
            id = PhotonNetwork.LocalPlayer.ActorNumber;
            
        imAssassin = false; 
        SetLocalPlayerAsAssassin();
    }
    private void Update()
    {
        
    }
    public void SetLocalPlayerAsAssassin()
    {
        Debug.Log("FUNCTION BEING CALLED IN START");
        if (PhotonNetwork.LocalPlayer.ActorNumber == mC.ORIOLMONGOLO)
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
