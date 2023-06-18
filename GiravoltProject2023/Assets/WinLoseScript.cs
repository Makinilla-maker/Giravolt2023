using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinLoseScript : MonoBehaviour
{
    public MainConnect mC;
    public GameObject menuC;
    public GameObject menuI;

    private void Awake()
    {
        mC = FindObjectOfType<MainConnect>();
        SetText();
    }

    public void SetText()
    {
        Debug.Log("SetText");
        if (mC._assassinWin == true)
        {
            Debug.Log("entering assassin wiu");
            menuI.SetActive(true);
            menuC.SetActive(false);
            Debug.Log("---------Assassin win in ending");
        }
        else
        {
            Debug.Log("---------else debug");
            menuI.SetActive(false);
            menuC.SetActive(true);
        }
    }
}
