using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
public class PlayerCameraFix : NetworkBehaviour
{
    GameObject camera1;
    Camera camera2;
    Camera camera3;
    Camera camera4;
    void Awake()
    {
        camera1 = GameObject.Find("CenterEyeAnchor");
        camera2 = GameObject.Find("LeftEyeAnchor").GetComponent<Camera>();
        camera3 = GameObject.Find("RightEyeAnchor").GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        camera1 = GameObject.Find("CenterEyeAnchor");
        // camera2 = GameObject.Find("LeftEyeAnchor").GetComponent<Camera>();
        // camera3 = GameObject.Find("RightEyeAnchor").GetComponent<Camera>();
        if (!IsOwner)
        {
            Debug.Log(IsOwner);
            camera1.SetActive(false);
        }
    }
}
