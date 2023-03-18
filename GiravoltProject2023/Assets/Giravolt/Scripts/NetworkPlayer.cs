using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;

public class NetworkPlayer : MonoBehaviour
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;
    private PhotonView photonView;

    private Transform headRig;
    private Transform leftHandRig;
    private Transform rightHandRig;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        photonView= GetComponent<PhotonView>();
        player = GameObject.Find("TrackerOffsets");
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
        {
            head.gameObject.SetActive(false);
            rightHand.gameObject.SetActive(false);
            leftHand.gameObject.SetActive(false);

            MapPosition(head, XRNode.Head);
            MapPosition(leftHand, XRNode.LeftHand);
            MapPosition(rightHand, XRNode.RightHand);

            this.gameObject.transform.position = player.transform.position;
            this.gameObject.transform.rotation = player.transform.rotation;
        }
    }
    void MapPosition(Transform target, XRNode node)
    {
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);

        Debug.Log(position + " " + rotation);

        target.localPosition = position;
        target.localRotation = rotation;
    }
}
