using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] GameObject UI;
    private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jump = 1f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    public override void OnNetworkSpawn()
    {

    }
    void Start()
    {
        UI = GameObject.Find("FirstNetWorkCanvas");
        UI.SetActive(false);
    }
    void Update()
    {
        if (!IsOwner)
        {
            Debug.Log(IsOwner);
            camera.enabled = false;
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}



