using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlacementTasks : MonoBehaviour
{
    private Rigidbody rb;
    private RigidbodyConstraints originalConstraints;
    private TaskManager manager;
    private bool doUpdate;
    [SerializeField]private ParticleSystem ps;
    private string taskName;
    private string taskDescription;
    public int id;
    // this code is for this script only and will only be used if this task is added to tasksForThisGame list
    public Task placementTask_01 = new Task();
    private string tagForThisTask;
    public bool canWipeAgain;
    public float timer = .3f;
    private float timerRestart;
    public bool canWipe = false;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        manager = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        ps = GetComponentInChildren<ParticleSystem>();
        taskDescription = "Tita";
        tagForThisTask = this.gameObject.tag;
        taskName = this.gameObject.name;
        string tmp = this.gameObject.tag;
        string newTexttext = tmp.Replace("Task_", "");
        GetComponentInChildren<TextMeshPro>().text = newTexttext;
        rb.useGravity = true;
        UnFreezeRigidboydConstraints();
        canWipeAgain = true;
        timerRestart = timer;
        // we go to the task manager to generate the task and assign its info
        
    }

    public void FreezeRigidbodyConstraints()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    public void UnFreezeRigidboydConstraints()
    {
        rb.constraints = originalConstraints;
    }

    IEnumerator CreateTask()
    {
        doUpdate = true;
        Debug.Log("Creating placement task!");
        yield return new WaitForSeconds(.2f);
        placementTask_01 = manager.CreateTask(taskName, taskDescription, TaskStatus.NOTSTARTED, this.gameObject, this.gameObject, id);
    }
        // Update is called once per frame
    void Update()
    {
        if (!doUpdate)
        {
            StartCoroutine(CreateTask());
        }
        if(canWipe && !canWipeAgain)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                canWipeAgain=true;
                timer = timerRestart;
            }
        }
    }
    public void OnCompletedTask()
    {
        ps.Play();
        
        StartCoroutine(Delay());
    }
    void OnTriggerEnter(Collider other)
    {
        if(placementTask_01.status.ToString() != "")
        {
            Debug.Log("This is my task status -> " + placementTask_01.status.ToString());
        }
        else
        {
            Debug.Log("Task status is null!");
        }
        
        if(other.gameObject.tag == tagForThisTask && placementTask_01.status != TaskStatus.COMPLETED)
        {
            if(placementTask_01.id == 13) // Sang
            {
                if(manager.ammountOfWipesSang != 0 && canWipeAgain)
                {
                    manager.photonView.RPC("DecreaseWipe", Photon.Pun.RpcTarget.All, 13);
                    canWipeAgain = false;
                }
                else
                {
                    if(manager.ammountOfWipesSang == 0)
                    {
                        placementTask_01.status = TaskStatus.COMPLETED;
                        manager.GetCompletedTask(placementTask_01);
                    }
                }
            }
            else if(placementTask_01.id == 8) // Taques
            {
                if(manager.ammountOfWipesTaques != 0 && canWipeAgain)
                {
                    manager.photonView.RPC("DecreaseWipe", Photon.Pun.RpcTarget.All,8);
                    canWipeAgain = false;
                }
                else
                {
                    if(manager.ammountOfWipesTaques == 0)
                    {
                        placementTask_01.status = TaskStatus.COMPLETED;
                        manager.GetCompletedTask(placementTask_01);
                    }
                }
            }
            else if (placementTask_01.id == 9) // Taques
            {
                if (manager.ammountOfTatxades != 0 && canWipeAgain)
                {
                    manager.photonView.RPC("DecreaseWipe", Photon.Pun.RpcTarget.All, 9);
                    canWipeAgain = false;
                }
                else
                {
                    if (manager.ammountOfTatxades == 0)
                    {
                        placementTask_01.status = TaskStatus.COMPLETED;
                        manager.GetCompletedTask(placementTask_01);
                    }
                }
            }
            else
            {
                placementTask_01.status = TaskStatus.COMPLETED;
                manager.GetCompletedTask(placementTask_01);
            }
        }
    }
   
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        GameObject go;
        go = GameObject.Find(taskName + "_Main");
        go.SetActive(false);        
    }
}
