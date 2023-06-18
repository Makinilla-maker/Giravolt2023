using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine.SceneManagement;
public class RolesManager : MonoBehaviour
{
    public int id;
    [SerializeField] public  bool imAssassin;
    [SerializeField] public bool imDead;
    public MainConnect mC;
    private bool doOnce = false;

    public bool assassinWin;

    private void Awake()
    {
        mC = FindObjectOfType<MainConnect>();
        imAssassin = false;
        imDead = false;
        assassinWin = false;
        id = PhotonNetwork.LocalPlayer.ActorNumber;
        GameObject.Find("Network Player(Clone)").GetComponent<CapsuleCollider>().enabled = false;
        SetLocalId();
    }
    private void Update()
    {
        
    }
    public void SetLocalId()
    {

        if (PhotonNetwork.LocalPlayer.ActorNumber == mC.ORIOLMONGOLO)
        {
            imAssassin = true;
            Debug.Log("I AM the ASSASSIN");
        }
        else
        {
            imAssassin = false;
            Debug.Log("I am NOT the ASSASSIN");
        }
    }

    public void KillMe()
    {
        Debug.Log(".......You are dead");
        imDead = true;
    }
}
