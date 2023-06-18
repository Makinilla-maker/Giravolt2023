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
    }

    public void SetText()
    {
        if (mC._assassinWin == true)
        {
            GameObject.Find("Menu Guanya l'assassi").SetActive(true);
            GameObject.Find("Menu Guanyen els convidats").SetActive(false);
        }
        else
        {
            GameObject.Find("Menu Guanya l'assassi").SetActive(false);
            GameObject.Find("Menu Guanyen els convidats").SetActive(true);
        }
    }
}
