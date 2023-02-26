using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PhotonObject : MonoBehaviour
{
    private PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BeforeGrab()
    {
        photonView.RequestOwnership();
    }
}
