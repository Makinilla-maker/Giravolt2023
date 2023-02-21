using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
public class PlayerCameraFix : NetworkBehaviour
{
    [SerializeField] private GameObject[] camera1;
    [SerializeField] private GameObject deleteCamera;
    Camera camera3;
    Camera camera4;
    void Start()
    {
        if(GameObject.Find("DeleteCamera") != null)
        {
            deleteCamera = GameObject.Find("DeleteCamera");
            deleteCamera.SetActive(false);
        }
        
        camera1 = GameObject.FindGameObjectsWithTag("OVRCameraRig");
        for(int i = 0; i < camera1.Length; i++)
        {
            if(!IsOwner)
                camera1[i].SetActive(false);
        }
            
    }
    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < camera1.Length; i++)
        {
            if(!IsOwner)
                camera1[i].SetActive(false);
        }
    }
}
