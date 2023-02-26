using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] Camera camera;
    void Update()
    {
        if(camera == null)
        {
            camera = GameObject.Find("MainCameraGo").GetComponent<Camera>();
        }
        if (!IsOwner)
        {
            if(camera != null)
                camera.enabled = false;
        }
    }
}



