using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using Photon;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine.Serialization;
using System.IO;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class DELETEME : MonoBehaviour
{
    MainConnect mc;
    // Start is called before the first frame update
    void Start()
    {
        mc = FindObjectOfType<MainConnect>();
        //mc.POLLA();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
