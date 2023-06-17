using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsSwitch : MonoBehaviour
{
    [SerializeField] public bool areLightsOn;
    [SerializeField] private TaskManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        areLightsOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player_Hand"))
        {
            switch (areLightsOn)
            {
                case true:
                    manager.CallTurnLights(true, this.gameObject.name);
                    break;
                case false:
                    manager.CallTurnLights(false, this.gameObject.name);
                    break;
                default:
                    break;
            }
            Debug.Log("SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");
        }
    }
    public void ChangeCubeColor(Color g)
    {
        GetComponent<MeshRenderer>().material.color = g;
    }
    
}
