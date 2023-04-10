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
    private string sendTaskName = "";
    private int sendTaskInt = -1;
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
                SendTaskStatus(currentTask.name, (int)TaskStatus.COMPLETED);
                currentTask.status = TaskStatus.COMPLETED;
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
    }
    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            if (sendTaskName != "")
            {
                stream.SendNext(sendTaskName);
                stream.SendNext(sendTaskInt);
                Debug.Log("sending this info: " + sendTaskName + " , " + sendTaskInt);
            }
            else
            {
                Debug.Log("Sending null information");
            }

        }
        else
        {
            // Network player, receive data
            this.sendTaskName = (string)stream.ReceiveNext();
            this.sendTaskInt = (int)stream.ReceiveNext();
            Debug.Log("receiving info: " + this.sendTaskName);
            CheckTasksState(this.sendTaskName, this.sendTaskInt);
        }
    }
    #endregion
    private void CheckTasksState(string name, int status)
    {
        Debug.Log("This is the last solved task name: " + name + " and this is the status of the task: " + (TaskStatus)status);
    }
}
