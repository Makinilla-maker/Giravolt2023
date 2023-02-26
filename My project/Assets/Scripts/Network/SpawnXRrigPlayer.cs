using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;

public class SpawnXRrigPlayer : NetworkBehaviour
{
    [SerializeField] GameObject xRig;
    [SerializeField] GameObject cameraOffset;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject rightController;
    [SerializeField] GameObject xrControllerRight;
    [SerializeField] GameObject leftController;
    [SerializeField] GameObject xrControllerLeft;
    GameObject xRigGo;
    GameObject cameraOffsetGo;
    GameObject mainCameraGo;
    GameObject rightControllerGo;
    GameObject xrControllerRightGo;
    GameObject leftControllerGo;
    GameObject xrControllerLeftGo;
    public GameObject player;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject SpawnPlayer()
    {
        xRigGo = Instantiate(xRig, transform.position, Quaternion.identity);
        cameraOffsetGo = Instantiate(cameraOffset, transform.position, Quaternion.identity, xRigGo.transform);
        xRigGo.AddComponent<NetworkObject>();
        xRigGo.AddComponent<NetworkTransform>();

        mainCameraGo = Instantiate(mainCamera, transform.position, Quaternion.identity, cameraOffsetGo.transform);
        mainCameraGo.name = "MainCameraGo";
        
        mainCameraGo.AddComponent<NetworkObject>();
        mainCameraGo.AddComponent<NetworkTransform>();
        rightControllerGo = Instantiate(rightController, transform.position, Quaternion.identity, xRigGo.transform);
        rightControllerGo.AddComponent<NetworkObject>();
        rightControllerGo.AddComponent<NetworkTransform>();
        xrControllerRightGo = Instantiate(xrControllerRight, transform.position, Quaternion.identity, rightControllerGo.transform);
        xrControllerRightGo.AddComponent<NetworkObject>();
        xrControllerRightGo.AddComponent<NetworkTransform>();
        leftControllerGo = Instantiate(leftController, transform.position, Quaternion.identity, xRigGo.transform);
        leftControllerGo.AddComponent<NetworkObject>();
        leftControllerGo.AddComponent<NetworkTransform>();
        xrControllerLeftGo = Instantiate(xrControllerLeft, transform.position, Quaternion.identity, leftControllerGo.transform);
        xrControllerLeftGo.AddComponent<NetworkObject>();
        xrControllerLeftGo.AddComponent<NetworkTransform>();

        return xRigGo;
    }
}
