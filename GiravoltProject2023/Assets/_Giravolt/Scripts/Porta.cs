using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    public List<GameObject> portaList;
    public GameObject selectedClau;
    // Start is called before the first frame update
    void Start()
    {
        int a = Random.Range(0,2);
        selectedClau = portaList[a].gameObject;
        foreach (GameObject item in portaList)
        {
            if(item != selectedClau)
            {
                selectedClau.GetComponent<PlacementTasks>().enabled = false;
            }
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
