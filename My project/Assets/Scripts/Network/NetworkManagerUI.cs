using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;
public class NetworkManagerUI : MonoBehaviour
{
    private TestRelay relay;
    public TMP_InputField inputField;
    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        relay = GameObject.Find("TestRelay").GetComponent<TestRelay>();
        serverButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartServer();
            //canvas.enabled = false;
        });
        hostButton.onClick.AddListener(() => {
            //NetworkManager.Singleton.StartHost();
            //canvas.enabled = false;
            relay.CreateRelay();
        });
        clientButton.onClick.AddListener(() => {
            Debug.Log("Input field text: " + inputField.text);
            NetworkManager.Singleton.StartClient();
            relay.JoinRelay(inputField.text);
            //canvas.enabled = false;
        });
    }
}
public class NetManagerUI : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        //this.gameObject.active = false;
    }
}
