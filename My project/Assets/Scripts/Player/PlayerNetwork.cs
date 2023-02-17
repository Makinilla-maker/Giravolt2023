using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] Camera camera;
    private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public override void OnNetworkSpawn()
    {

    }
    void Update()
    {
        if (!IsOwner)
        {
            Debug.Log(IsOwner);
            camera.enabled = false;
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(OwnerClientId + ";" + randomNumber.Value);
        }
        Vector3 moveDir = new Vector3(0, 0, 0);
        if(Input.GetKey(KeyCode.W))
        {
            moveDir.z += 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDir.x += -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir.x += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDir.z += -1f;
        }

        float moveSpeed = 5f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
