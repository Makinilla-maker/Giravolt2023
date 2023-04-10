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
    GAMEOBJECT,
    NONE,
}
[System.Serializable]

public class TaskManager : MonoBehaviourPunCallbacks, IPunObservable
{
    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            // We own this player: send the others our data
           if(sendTask != null)
           {
             stream.SendNext(sendTask);
           }
            
        }
        else
        {
            // Network player, receive data
            this.ReceiveTaskStatus((Task)stream.ReceiveNext());
        }
    }

    #endregion
    public List<Task> tasks = new List<Task>();
    public GameObject go;
    int taskCompleted = 0;
    private PhotonView pView;
    private Task sendTask;
    private void Awake()
    {
        pView = GetComponent<PhotonView>();
    }
    public void OnPlace(GameObject receptor)
    {
        go = receptor.transform.GetChild(receptor.transform.childCount - 1).gameObject;

        Debug.Log(go.name);
        foreach(Task task in tasks)
        {
            Debug.Log(task.mainObject.name + "==" + go.name);
            Debug.Log(task.targetObject.name + "==" + receptor.name);
            if (task.mainObject.name == go.name && task.targetObject.name == receptor.name)
            {
                task.status = TaskStatus.COMPLETED;
                SendTaskStatus(task);
            }
        }
    }
    private void SendTaskStatus(Task task)
    {
        sendTask = task;
    }
    private void ReceiveTaskStatus(Task taskReceive)
    {
        foreach (Task task in tasks)
        {
            if (task.id == taskReceive.id)
            {
                task.description = taskReceive.description;
                task.status = taskReceive.status;
                task.name = taskReceive.name;
                task.mainObject = taskReceive.mainObject;
                Debug.Log("this is the current name of the task" + task.name);
            }
        }
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
}
