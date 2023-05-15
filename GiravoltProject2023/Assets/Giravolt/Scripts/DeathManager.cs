using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{

    [SerializeField] public bool isAlive = true;
    [SerializeField] Material deathMat;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive == false)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = deathMat;
        }
    }
}
