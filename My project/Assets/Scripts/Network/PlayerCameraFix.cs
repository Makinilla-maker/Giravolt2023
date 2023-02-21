using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
public class PlayerCameraFix : NetworkBehaviour
{
    [SerializeField] private GameObject[] cameras;
    [SerializeField] private GameObject deleteCamera;
    [SerializeField] private GameObject hostCamera;
    [SerializeField] private GameObject clientCamera;
    void Start()
    {
        if(GameObject.Find("DeleteCamera") != null)
        {
            deleteCamera = GameObject.Find("DeleteCamera");
            deleteCamera.SetActive(false);
        }
        
        cameras = GameObject.FindGameObjectsWithTag("OVRCameraRig");
        for(int i = 0; i < cameras.Length; i++)
        {
            if(IsHost)
            {
                hostCamera = cameras[i];
            }
            else if(IsClient)
            {
                clientCamera = cameras[i];
            }   
        }

        if(IsHost && IsOwner && clientCamera != null)
        {
            clientCamera.SetActive(false);
        }
        else if(IsOwner && IsClient && hostCamera != null)
        {
            hostCamera.SetActive(false);
        }
            
    }
}
