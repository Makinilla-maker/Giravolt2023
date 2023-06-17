using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    public List<GameObject> portaList;
    public GameObject selectedClau;
    // Start is called before the first frame update
    void Awake()
    {
        int a = Random.Range(0,2);
        selectedClau = portaList[a].gameObject;
        selectedClau.GetComponent<PlacementTasks>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
