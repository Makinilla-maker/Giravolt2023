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
    public GameObject xRigGo;
    public GameObject cameraOffsetGo;
    public GameObject mainCameraGo;
    public GameObject rightControllerGo;
    public GameObject xrControllerRightGo;
    public GameObject leftControllerGo;
    public GameObject xrControllerLeftGo;
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
        

        mainCameraGo = Instantiate(mainCamera, transform.position, Quaternion.identity, cameraOffsetGo.transform);
        mainCameraGo.name = "MainCameraGo";
        
        
        rightControllerGo = Instantiate(rightController, transform.position, Quaternion.identity, xRigGo.transform);
        
        xrControllerRightGo = Instantiate(xrControllerRight, transform.position, Quaternion.identity, rightControllerGo.transform);
        
        leftControllerGo = Instantiate(leftController, transform.position, Quaternion.identity, xRigGo.transform);
        
        xrControllerLeftGo = Instantiate(xrControllerLeft, transform.position, Quaternion.identity, leftControllerGo.transform);
        xrControllerLeftGo.AddComponent<NetworkObject>();
        xrControllerLeftGo.AddComponent<NetworkTransform>();

        return xRigGo;
    }
}
