using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
public class PlayerNetwork : NetworkBehaviour
{
    private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public override void OnNetworkSpawn()
    {
        randomNumber.OnValueChanged += (int previousValue, int newValue) => { };
        string s = OwnerClientId + "; + randomNumber" + randomNumber.Value;
        text.text = s;
    }
    void Update()
    {
        if (!IsOwner) return;

        if(Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(OwnerClientId + ";" + randomNumber.Value);
            randomNumber.Value = Random.Range(0, 100);
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
