using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine.SceneManagement;
public class RolesManager : MonoBehaviour
{
    public int id;
    [SerializeField] public  bool imAssassin;
    [SerializeField] public bool imDead;
    public MainConnect mC;
    private bool doOnce = false;

    public bool assassinWin;

    [SerializeField] public GameObject rolePaper;

    public Material langMat;

    public LanguageSelector languageSelector;

    private void Awake()
    {
        mC = FindObjectOfType<MainConnect>();
        imAssassin = false;
        imDead = false;
        assassinWin = false;
        id = PhotonNetwork.LocalPlayer.ActorNumber;
        GameObject.Find("Network Player(Clone)").GetComponent<CapsuleCollider>().enabled = false;
        SetLocalId();
        
        languageSelector = mC.GetComponent<LanguageSelector>();
        
        SetMyMat();

        Instantiate(rolePaper, new Vector3(transform.position.x, transform.position.y, (transform.position.z + 1)), Quaternion.identity);
    }
    private void Update()
    {
        
    }
    public void SetLocalId()
    {

        if (PhotonNetwork.LocalPlayer.ActorNumber == mC.ORIOLMONGOLO)
        {
            imAssassin = true;
            Debug.Log("I AM the ASSASSIN");
        }
        else
        {
            imAssassin = false;
            Debug.Log("I am NOT the ASSASSIN");
        }
    }

    public void KillMe()
    {
        Debug.Log(".......You are dead");
        imDead = true;
    }

    public void SetMyMat()
    {
        Debug.Log(languageSelector.lang);
        switch (languageSelector.lang)
        {
            case 1:
                if (imAssassin == false)
                {
                    langMat = GameObject.Find("LanguagesMats").GetComponent<LanguagesMats>().C_Cat;
                }
                else
                {
                    langMat = GameObject.Find("LanguagesMats").GetComponent<LanguagesMats>().I_Cat;
                }
                break;
            case 2:
                if (imAssassin == false)
                {
                    langMat = GameObject.Find("LanguagesMats").GetComponent<LanguagesMats>().C_Esp;
                }
                else
                {
                    langMat = GameObject.Find("LanguagesMats").GetComponent<LanguagesMats>().I_Esp;
                }
                break;
            case 3:
                if (imAssassin == false)
                {
                    langMat = GameObject.Find("LanguagesMats").GetComponent<LanguagesMats>().C_Eng;
                }
                else
                {
                    langMat = GameObject.Find("LanguagesMats").GetComponent<LanguagesMats>().I_Eng;
                }
                break;

        }
        rolePaper.GetComponent<MeshRenderer>().material = langMat;
    }
}
