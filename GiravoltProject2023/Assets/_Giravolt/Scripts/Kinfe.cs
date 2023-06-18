using Autohand.Demo;
using Autohand;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Oculus.Interaction;

public class Kinfe : MonoBehaviour
{
    public LayerMask stabbableLayers;
    [SerializeField] public bool isStabbing;
    public RolesManager rolesManager;
    [SerializeField] private float speed;
    [SerializeField] public GameObject knife;
    public string hitName;
    public int hitID;
    [SerializeField] public GameObject maskPrefab;


    public int alivePlayers;
    public PhotonView pView;

    void Start()
    {
        isStabbing = false;
        rolesManager = GameObject.Find("RoleManager").GetComponent<RolesManager>();
        pView = GetComponent<PhotonView>();
        alivePlayers = PhotonNetwork.CurrentRoom.Players.Count;
        SetGrabbable();
    }

    public void SetGrabbable()
    {
        if (rolesManager.imAssassin == true)
        {
            this.GetComponent<Autohand.Grabbable>().enabled = true;
        }
        else
        {
            this.GetComponent<Autohand.Grabbable>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (isStabbing == false && speed >= 0.5f && other.tag == "Player")
        if (other.tag == "Player" && rolesManager.imAssassin == true)
        {
            isStabbing = true;
            Debug.Log("-------------you stabbed " + other.gameObject.name);
            hitName = other.gameObject.name;
            hitID = int.Parse(hitName.Substring(hitName.Length - 1));
            Debug.Log("--------KiledID = " + hitID);
            pView.RPC("KillId", RpcTarget.All, hitID, hitName);
        }
        else isStabbing = false;
    }

    [PunRPC]
    public void KillId(int killedId, string killedName)
    {
        Debug.Log("------Player " + killedId + "was killed");
        if(rolesManager.id == killedId) 
        {
            Debug.Log("-----You are being killed");
            rolesManager.KillMe();
        }
        string path = $"{killedName}/Head/Robot Kyle/Robot2";
        //Debug.Log(path);
        GameObject.Find(path).SetActive(false);
        Instantiate(maskPrefab, GameObject.Find(path).transform.position, Quaternion.identity);
        --alivePlayers;
        if (alivePlayers <= 1)
        {
            rolesManager.assassinWin = true;
        }

    }
}
