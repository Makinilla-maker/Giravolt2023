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
   
    public List<Task> tasks = new List<Task>();
    public GameObject go;
    int taskCompleted = 0;
    private PhotonView pView;
    private string sendTask;
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
                SendTaskStatus(currentTask.name);
                currentTask.status = TaskStatus.COMPLETED;
            }
        }
    }
    private void SendTaskStatus(string task)
    {
        sendTask = task;
    }
    private string ReceiveTaskStatus(string taskReceive)
    {
        string ret = "";
        foreach (Task task in tasks)
        {
            if (task.name == taskReceive)
            {
                ret = task.name;
                Debug.Log("this is the current name of the task" + task.name);
            }
            else
            {
                ret = "";
            }
        }
        return ret;
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
            if (sendTask != null)
            {
                stream.SendNext(sendTask);
                Debug.Log("sending this info: " + sendTask);
            }
            else
            {
                Debug.Log("Sending null information");
            }

        }
        else
        {
            Debug.Log("receiving info: ");
            // Network player, receive data
            this.sendTask = (string)stream.ReceiveNext();
        }
    }
    #endregion
}
