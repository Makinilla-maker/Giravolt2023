using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementTasks : MonoBehaviour
{
    private Rigidbody rb;
    private RigidbodyConstraints originalConstraints;
    private TaskManager manager;
    private bool doUpdate;
    [SerializeField]private ParticleSystem ps;
    // this code is for this script only and will only be used if this task is added to tasksForThisGame list
    public Task placementTask_01 = new Task();

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        manager = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        ps = GetComponentInChildren<ParticleSystem>();
        rb.useGravity = false;
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
        yield return new WaitForSeconds(3f);
        placementTask_01 = manager.CreateTask("Placement Task 1", "This is a random generic placement task", TaskStatus.NOTSTARTED, this.gameObject, this.gameObject, 4);
    }
        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                rb.useGravity = true;
            }

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
        if(other.gameObject.tag == "Task_1" && placementTask_01.status != TaskStatus.COMPLETED)
        {
            placementTask_01.status = TaskStatus.COMPLETED;
            manager.GetCompletedTask(placementTask_01);
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }
}
