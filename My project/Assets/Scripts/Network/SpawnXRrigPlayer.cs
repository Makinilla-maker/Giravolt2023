using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;
using Unity.Netcode.Transports.UTP;

public class SpawnXRrigPlayer : NetworkBehaviour
{
    [SerializeField] GameObject xRig;
    [SerializeField] GameObject cameraOffset;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject rightController;
    [SerializeField] GameObject xrControllerRight;
    [SerializeField] GameObject leftController;
    [SerializeField] GameObject xrControllerLeft;
    public GameObject xRigGo;
    public GameObject cameraOffsetGo;
    public GameObject mainCameraGo;
    public GameObject rightControllerGo;
    public GameObject xrControllerRightGo;
    public GameObject leftControllerGo;
    public GameObject xrControllerLeftGo;



    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnPlayer()
    {
        xRigGo = Instantiate(xRig, transform.position, Quaternion.identity);
        cameraOffsetGo = Instantiate(cameraOffset, transform.position, Quaternion.identity, xRigGo.transform);
        mainCameraGo = Instantiate(mainCamera, transform.position, Quaternion.identity, cameraOffsetGo.transform);
        mainCameraGo.name = "MainCameraGo";
        
        
        rightControllerGo = Instantiate(rightController, transform.position, Quaternion.identity, xRigGo.transform);
        
        xrControllerRightGo = Instantiate(xrControllerRight, transform.position, Quaternion.identity, rightControllerGo.transform);
        
        leftControllerGo = Instantiate(leftController, transform.position, Quaternion.identity, xRigGo.transform);
        xrControllerLeftGo = Instantiate(xrControllerLeft, transform.position, Quaternion.identity, leftControllerGo.transform);
        xRigGo.AddComponent<NetworkObject>();
        xRigGo.AddComponent<NetworkTransform>();
        xRigGo.GetComponent<NetworkObject>().Spawn();



        cameraOffsetGo.AddComponent<NetworkObject>();
        cameraOffsetGo.AddComponent<NetworkTransform>();
        cameraOffsetGo.GetComponent<NetworkObject>().Spawn();
        cameraOffsetGo.transform.SetParent(xRigGo.transform, false);



        mainCameraGo.AddComponent<NetworkObject>();
        mainCameraGo.AddComponent<NetworkTransform>();
        mainCameraGo.GetComponent<NetworkObject>().Spawn();
        mainCameraGo.transform.SetParent(cameraOffsetGo.transform, false);

        rightControllerGo.AddComponent<NetworkObject>();
        rightControllerGo.AddComponent<NetworkTransform>();
        rightControllerGo.GetComponent<NetworkObject>().Spawn();
        rightControllerGo.transform.SetParent(xRigGo.transform, false);

        xrControllerRightGo.AddComponent<NetworkObject>();
        xrControllerRightGo.AddComponent<NetworkTransform>();
        xrControllerRightGo.GetComponent<NetworkObject>().Spawn();
        xrControllerRightGo.transform.SetParent(rightControllerGo.transform, false);


        leftControllerGo.AddComponent<NetworkObject>();
        leftControllerGo.AddComponent<NetworkTransform>();
        leftControllerGo.GetComponent<NetworkObject>().Spawn();
        leftControllerGo.transform.SetParent(xRigGo.transform, false);


        xrControllerLeftGo.AddComponent<NetworkObject>();
        xrControllerLeftGo.AddComponent<NetworkTransform>();
        xrControllerLeftGo.GetComponent<NetworkObject>().Spawn();
        xrControllerLeftGo.transform.SetParent(leftControllerGo.transform, false);
    }
}
