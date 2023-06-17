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

    public PhotonView pView;

    void Start()
    {
        isStabbing = false;
        rolesManager = GameObject.Find("RoleManager").GetComponent<RolesManager>();
        pView = GetComponent<PhotonView>();

        SetGrabbable();
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(CalculateSpeed());
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

    //IEnumerator CalculateSpeed()
    //{
    //    Vector3 lastPos = transform.position;
    //    yield return new WaitForFixedUpdate();
    //    speed = (lastPos- transform.position).magnitude / Time.deltaTime;
    //}

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
        string path = $"{hitName}/Head/Robot Kyle/Robot2";
        Debug.Log(path);
        Debug.Log(transform.Find(path).GetComponent<SkinnedMeshRenderer>().enabled);
        //transform.Find(hitName + "/Head/Robot Kyle/Robot2").GetComponent<SkinnedMeshRenderer>().enabled = false;
    }
}
