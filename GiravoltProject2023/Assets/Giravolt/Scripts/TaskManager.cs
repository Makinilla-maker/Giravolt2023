using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using Photon;
using Photon.Pun;
[System.Serializable]
public enum TaskStatus
{
    NOTSTARTED,
    DOING,
    COMPLETED,
    NONE,
}
[System.Serializable]

public class TaskManager : MonoBehaviourPunCallbacks, IPunObservable
{
   
    public List<Task> tasks = new List<Task>();
    public GameObject go;
    int taskCompleted = 0;
    private string sendTaskName = "Tita";
    private int sendTaskInt = -1;
    private PhotonView pView;
    public GameObject ball;
    public bool goingLeft = false;
    private bool onlineGoingLeft = false;
    private void Awake()
    {
        pView = GetComponent<PhotonView>();
    }
    public void OnPlace(GameObject receptor)
    {
        go = receptor.transform.GetChild(receptor.transform.childCount - 1).gameObject;

        Debug.Log(go.name);
        foreach(Task currentTask in tasks)
        {
            Debug.Log(currentTask.mainObject.name + "==" + go.name);
            Debug.Log(currentTask.targetObject.name + "==" + receptor.name);
            if (currentTask.mainObject.name == go.name && currentTask.targetObject.name == receptor.name)
            {
                
                currentTask.status = TaskStatus.COMPLETED;
                if (pView.IsMine)
                {
                    // We own this player: send the others our data
                    if (sendTaskName != "")
                    {
                        sendTaskName = currentTask.name;
                        sendTaskInt = currentTask.id;
                        Debug.Log("sending this info: " + sendTaskName + " , " + sendTaskInt);
                    }
                    else
                    {
                        Debug.Log("Sending null information");
                    }

                }
                else
                {
                    pView.RPC("ApplyReceivedChanges", pView.Owner, sendTaskName, sendTaskInt);
                }
            }
        }
    }
    private void SendTaskStatus(string task, int status)
    {
        sendTaskName = task;
        sendTaskInt = status;
    }
    private void Update()
    {
        foreach (Task task in tasks)
        {
            if (task.status == TaskStatus.COMPLETED)
                taskCompleted++;
        }
        if(taskCompleted == tasks.Count)
        {
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        }
        if (goingLeft)
        {
            ball.GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0);
        }
        else
        {
            ball.GetComponent<Rigidbody>().velocity = new Vector3(-1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            goingLeft = !goingLeft;
            if (pView.IsMine)
            {
                // We own this player: send the others our data
                if (sendTaskName != "")
                {
                    sendTaskName = "POLLA";
                    sendTaskInt = -1;
                    onlineGoingLeft = goingLeft;
                    Debug.Log("sending this info: " + sendTaskName + " , " + sendTaskInt);
                    
                        
                }
                else
                {
                    Debug.Log("Sending null information");
                }

            }
            else
            {
                pView.RPC("ApplyReceivedChanges", pView.Owner, sendTaskName, sendTaskInt, onlineGoingLeft);
            }
        }
    }
    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }
    [PunRPC]
    public void ApplyReceivedChanges(string task, int id, bool go)
    {
        // Network player, receive data
        this.sendTaskName = task;
        this.sendTaskInt = id;
        Debug.Log("receiving info: " + this.sendTaskName);
        CheckTasksState(this.sendTaskName, this.sendTaskInt);
        this.goingLeft = onlineGoingLeft;

    }
    #endregion
    private void CheckTasksState(string name, int status)
    {
        Debug.Log("This is the last solved task name: " + name + " and this is the status of the task: " + (TaskStatus)status);
    }
}
