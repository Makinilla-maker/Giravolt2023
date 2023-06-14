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
    [SerializeField] bool imAssassin;
    public MainConnect mC;
    private bool doOnce = false;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        mC = FindObjectOfType<MainConnect>();
        imAssassin = false;
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
