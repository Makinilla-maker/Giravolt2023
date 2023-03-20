using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Autohand;
using Photon.Pun;
using Photon.Realtime;
public class NetworkGameObjects : Grabbable
{
    // Start is called before the first frame update
    public void ChangeOwnership(GameObject obj)
    {
        if (obj.GetComponent<PhotonView>())
        {
            if (!obj.GetComponent<PhotonView>().AmOwner)
                obj.GetComponent<PhotonView>().TransferOwnership(obj.gameObject.GetComponent<PhotonView>().Controller);
        }
    }
}
