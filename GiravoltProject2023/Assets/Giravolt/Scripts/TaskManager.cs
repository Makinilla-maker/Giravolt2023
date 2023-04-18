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
    private string reciveTaskName = "KIHJDIOSIHDISAJDIASN";
    private int sendTaskInt = -1;
    private int reciveTaskInt = -2;
    public bool sendGoingLeft = false;
    private bool reciveGoingLeft = true;
    private PhotonView pView;
    public GameObject ball;
    private bool send = false;
    private void Awake()
    {
        pView = GetComponent<PhotonView>();
        send = false;
        
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pView.RequestOwnership();                
            
        }
        //if (sendGoingLeft)
        //{
        //    ball.GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0);
        //}
        //else
        //{
        //    ball.GetComponent<Rigidbody>().velocity = new Vector3(-1, 0, 0);
        //}
    }
    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (pView.IsMine)
        {
            
            sendGoingLeft = !sendGoingLeft;
            
            // We own this player: send the others our data
            if (sendTaskName != "")
            {
                sendTaskName = "POLLA";
                sendTaskInt = -1;

                Debug.Log("Sending this info: " + sendTaskName + " , " + sendTaskInt + "\n" + "This is the value of the bool: " + sendGoingLeft);

                stream.SendNext(sendTaskName);
                stream.SendNext(sendTaskInt);
                stream.SendNext(sendGoingLeft);
            }
            else
            {
                Debug.Log("Reciving null information");
            }
        }
        else
        {
            //sendTaskName = (string)stream.ReceiveNext();
            //sendTaskInt = (int)stream.ReceiveNext();
            //sendGoingLeft = (bool)stream.ReceiveNext();
            Debug.Log("Received Task name: " + sendTaskName);
            //pView.RPC("ApplyReceivedChanges", RpcTarget.All, stream);
        }

    }
    [PunRPC]
    public void ApplyReceivedChanges(PhotonStream stream)
    {
        // Network player, receive data

        this.sendTaskName = (string)stream.ReceiveNext();
        this.sendTaskInt = (int)stream.ReceiveNext();
        this.sendGoingLeft = (bool)stream.ReceiveNext();
        CheckTasksState(this.sendTaskName, this.sendTaskInt);

    }
    #endregion
    private void CheckTasksState(string name, int status)
    {
        Debug.Log("This is the last solved task name: " + name + " and this is the status of the task: " + (TaskStatus)status + "\n" + "This is the value of the bool: " + sendGoingLeft);
    }
}
