using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
public class SetUserName : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputUserName;
    [SerializeField] private MainConnect mC;
    // Start is called before the first frame update
    void Start()
    {
        inputUserName = GameObject.Find("UserNameField").GetComponent<TMP_InputField>();
        mC = GameObject.Find("MainNetworkGameObject").GetComponent<MainConnect>();
    }
    // UserNameField
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OurSetUserName()
    {
        mC.userName = inputUserName.text;
        this.gameObject.SetActive(false);
    }
}
