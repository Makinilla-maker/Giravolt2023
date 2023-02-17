using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
public class PlayerCameraFix : NetworkBehaviour
{
    Camera camera;
    void Awake()
    {
        camera = GameObject.Find("CenterEyeAnchor").GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            Debug.Log(IsOwner);
            GetComponent<Camera>().enabled = false;
        }
    }
}
