using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private GameObject[] cleanUI;
    public GameObject clientUi;
    // Start is called before the first frame update
    void Start()
    {
        cleanUI = GameObject.FindGameObjectsWithTag("CleanUI");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator CleanMainUI()
    {
        foreach(GameObject ui in cleanUI)
        {
            ui.SetActive(false);
        }
        yield return new WaitForSeconds(.3f);
    }
    public void CleanAllUI()
    {
        CleanMainUI();
        CleanClientUI();
    }
    public void CleanClientUI()
    {
        clientUi.SetActive(false);
    }
    public void EnterClientUI()
    {
        CleanMainUI();
        clientUi.SetActive(true);
    }
}
