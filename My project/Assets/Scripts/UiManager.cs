using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private GameObject[] cleanUI;
    // Start is called before the first frame update
    void Start()
    {
        cleanUI = GameObject.FindGameObjectsWithTag("CleanUI");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CleanUI()
    {
        foreach(GameObject ui in cleanUI)
        {
            ui.SetActive(false);
        }
    }
}
