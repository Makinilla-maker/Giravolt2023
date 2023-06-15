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
            if(placementTask_01.id == 13) // Taques
            {
                if(manager.ammountOfWipes != 0)
                {
                    manager.sendWipeTask = true;
                }
                else
                {
                    if(manager.ammountOfWipes == 0)
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
        this.gameObject.SetActive(false);
    }
}
