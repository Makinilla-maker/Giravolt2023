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
    public int isaacMongolo = 0;
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
    public void SetLocalId()
    {
        id = PhotonNetwork.LocalPlayer.ActorNumber;
        if (PhotonNetwork.LocalPlayer.ActorNumber == isaacMongolo)
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
}
