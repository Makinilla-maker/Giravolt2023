using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WinLoseScript : MonoBehaviour
{
    public MainConnect mC;
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
            GameObject.Find("MenuI").SetActive(true);
            GameObject.Find("MenuC").SetActive(false);
            Debug.Log("---------Assassin win in ending");
        }
        else

            Debug.Log("---------else debug");
            GameObject.Find("MenuI").SetActive(false);
            GameObject.Find("MenuC").SetActive(true);
        }
    }
}
