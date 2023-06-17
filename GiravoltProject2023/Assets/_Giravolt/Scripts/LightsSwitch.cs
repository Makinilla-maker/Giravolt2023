using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsSwitch : MonoBehaviour
{
    [SerializeField] public bool areLightsOn;
    [SerializeField] private TaskManager manager;
    public bool canISwitch;
    float time = 2;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        areLightsOn = true;
        canISwitch = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!canISwitch)
        {
            time -= Time.deltaTime;
            if(time <= 0)
            {
                canISwitch = true;
                time = 3;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player_Hand") && canISwitch)
        {
            switch (areLightsOn)
            {
                case true:
                    manager.CallTurnLights(true, this.gameObject.name, canISwitch);
                    break;
                case false:
                    manager.CallTurnLights(false, this.gameObject.name, canISwitch);
                    break;
                default:
                    break;
            }
            canISwitch = false;
            Debug.Log("SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS");
        }
    }
    public void ChangeCubeColor(Color g)
    {
        GetComponent<MeshRenderer>().material.color = g;
    }
    
}
