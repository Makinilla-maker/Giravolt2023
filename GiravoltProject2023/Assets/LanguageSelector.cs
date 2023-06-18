using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSelector : MonoBehaviour
{
    public int lang;

    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);

        lang = 1;
    }

    public void ChangeCat()
    {
        lang = 1;
    }    
    
    public void ChangeEsp()
    {
        lang = 2;
    }       
    public void ChangeEng()
    {
        lang = 3;
    }    
}
