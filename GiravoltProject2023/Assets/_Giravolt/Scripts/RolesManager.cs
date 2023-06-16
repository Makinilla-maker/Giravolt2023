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
    private void Awake()
    {
        mC = FindObjectOfType<MainConnect>();
        imAssassin = false;
        imDead = false; 
        id = PhotonNetwork.LocalPlayer.ActorNumber;
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
        imDead = true;
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(2).gameObject.SetActive(false);
        Debug.Log("----------you got stabbed and Killed");
    }
}
