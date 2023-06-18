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
            GameObject.Find("Menu Guanya l'assassi").SetActive(true);
            GameObject.Find("Menu Guanyen els convidats").SetActive(false);
            Debug.Log("---------Assassin win in ending");
        }
        else
        {
            GameObject.Find("Menu Guanya l'assassi").SetActive(false);
            GameObject.Find("Menu Guanyen els convidats").SetActive(true);
        }
    }
}
