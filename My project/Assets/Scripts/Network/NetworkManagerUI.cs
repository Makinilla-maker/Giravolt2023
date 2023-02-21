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
    [SerializeField] private Button clientJoinButton;
    [SerializeField] private Canvas canvas;
    private UiManager uiManager;
    private void Awake()
    {
        uiManager = GameObject.Find("UiManager").GetComponent<UiManager>();
        canvas = GetComponent<Canvas>();
        relay = GameObject.Find("TestRelay").GetComponent<TestRelay>();
        serverButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartServer();
            StartCoroutine(uiManager.CleanMainUI());
            
        });
        hostButton.onClick.AddListener(() => {
            relay.CreateRelay();
        });
        clientButton.onClick.AddListener(() => {
            Debug.Log("Input field text: " + inputField.text);
            uiManager.EnterClientUI();
            StartCoroutine(uiManager.CleanMainUI());
        });
        clientJoinButton.onClick.AddListener(() => {
            Debug.Log("Input field text: " + inputField.text);
            NetworkManager.Singleton.StartClient();
            relay.JoinRelay(inputField.text);
            //uiManager.CleanMainUI();
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
