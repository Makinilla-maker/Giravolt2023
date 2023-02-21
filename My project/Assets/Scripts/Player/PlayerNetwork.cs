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
        if (!IsOwner)
        {
            camera.enabled = false;
        }
    }
}



