using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        serverButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartServer();
            //canvas.enabled = false;
        });
        hostButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
            //canvas.enabled = false;
        });
        clientButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
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
