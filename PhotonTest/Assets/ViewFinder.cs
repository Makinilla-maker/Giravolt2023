using Autohand;
using Autohand.Demo;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewFinder : MonoBehaviourPunCallbacks
{
    private PhotonView[] view;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckPhotonViews(GameObject go)
    {
        view  = GameObject.FindObjectsOfType<PhotonView>();
        for (int i = 0; i < view.Length; i++)
        {
            if (view[i].IsMine)
            {
                Debug.Log("============================================================ IS MINE ========================================");
                GetComponentInChildren<AutoHandPlayer>().enabled = true;
                //GetComponentInChildren<OVRManager>().enabled = true;
                GetComponentInChildren<Camera>().enabled = true;
                GetComponentInChildren<OVRHandControllerLink>().enabled = true;
                GetComponentInChildren<OVRControllerEvent>().enabled = true;
                GetComponentInChildren<OVRCameraRig>().enabled = true;
            }
            else
            {
                Debug.Log("============================================================ IS NOT MINE ========================================");
                GetComponentInChildren<CheckMe>().gameObject.SetActive(false);
                GetComponentInChildren<AutoHandPlayer>().enabled = false;
                GetComponentInChildren<Camera>().enabled = false;
                GetComponentInChildren<OVRHandControllerLink>().enabled = false;
                GetComponentInChildren<OVRControllerEvent>().enabled = false;
                GetComponentInChildren<OVRCameraRig>().enabled = false;
            }
        }
    }
}
