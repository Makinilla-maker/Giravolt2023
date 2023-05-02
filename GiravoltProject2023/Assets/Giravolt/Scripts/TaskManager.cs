using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using Photon;
using Photon.Pun;
using UnityEngine.Serialization;

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
    public Task taskCompleted;
    int tasksCompleted = 0;
    private string sendTaskName = "A";
    private int sendTaskInt = -1;
    public bool send = false;
    private PhotonView pView;
    public GameObject ball;
    
    
    // ISAAC
    public bool alreadyGeneratedListSend;
    public bool alreadyGeneratedListReceived;
    [SerializeField] private List<Task> allTasks = new List<Task>();
    [SerializeField] public List<Task> tasksForThisGame = new List<Task>();
    public int numberOfTasksForThisGame;
    private void Awake()
    {
        pView = GetComponent<PhotonView>();
        send = false;
        
        if (!alreadyGeneratedListSend)
        {
            for (int i = 0; i < numberOfTasksForThisGame; ++i)
            {
                int randomNumber = Random.Range(0, allTasks.Count);
                tasksForThisGame.Add(allTasks[randomNumber]);
            }
        }
    }
    public void OnPlace(GameObject receptor)
    {
        GameObject go = receptor.transform.GetChild(receptor.transform.childCount - 1).gameObject;

        Debug.Log(go.name);
        foreach(Task currentTask in tasks)
        {
            Debug.Log(currentTask.mainObject.name + "==" + go.name);
            Debug.Log(currentTask.targetObject.name + "==" + receptor.name);
            if (currentTask.mainObject.name == go.name && currentTask.targetObject.name == receptor.name && currentTask.status != TaskStatus.COMPLETED)
            {
                currentTask.status = TaskStatus.COMPLETED;
                taskCompleted = currentTask;
                pView.RequestOwnership();
                send = true;
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
                tasksCompleted++;
        }
        if(tasksCompleted == tasks.Count)
        {
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pView.RequestOwnership();
            send = true;
        }
        
    }
    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (pView.IsMine)
        {
            if (!alreadyGeneratedListSend)
            {
                stream.SendNext(alreadyGeneratedListSend);
                stream.SendNext(numberOfTasksForThisGame);
                for (int i = 0; i < tasksForThisGame.Count; ++i)
                {
                    stream.SendNext(tasksForThisGame[i].name);
                    stream.SendNext(tasksForThisGame[i].description);
                    stream.SendNext((int)tasksForThisGame[i].status);
                    stream.SendNext(tasksForThisGame[i].mainObject.name);
                    stream.SendNext(tasksForThisGame[i].targetObject.name);
                    stream.SendNext(tasksForThisGame[i].id);
                }

                alreadyGeneratedListSend = true;
                alreadyGeneratedListReceived = false;
            }
            
            if(send)
            {
                // We own this player: send the others our data
                if (sendTaskName != "")
                {
                    sendTaskName = taskCompleted.name;
                    sendTaskInt = taskCompleted.id;

                    Debug.Log("Sending this info: " + sendTaskName + " , " + sendTaskInt + "\n" + "This is the value of the bool: ");

                    stream.SendNext(sendTaskName);
                    stream.SendNext(sendTaskInt);
                }
                else
                {
                    Debug.Log("Sending null information");
                }
                send = !send;
            }        
        }
        else
        {
            if (!alreadyGeneratedListReceived)
            {
                if (!pView.IsMine)
                {
                    alreadyGeneratedListSend = (bool)stream.ReceiveNext();
                    int _numberOfTasksForThisGame = (int)stream.ReceiveNext();
                    for (int i = 0; i < _numberOfTasksForThisGame; ++i)
                    {
                        Task newTask = new Task();
                        newTask.name = (string)stream.ReceiveNext();
                        newTask.description = (string)stream.ReceiveNext();
                        int s = (int)stream.ReceiveNext();
                        newTask.status = (TaskStatus)s;
                        string s1 = (string)stream.ReceiveNext();
                        newTask.mainObject = GameObject.Find(s1);
                        string s2 = (string)stream.ReceiveNext();
                        newTask.targetObject = GameObject.Find(s2);
                        newTask.id = (int)stream.ReceiveNext();
                        tasksForThisGame.Add(newTask);
                    }
                }
                
            }
            
            sendTaskName = (string)stream.ReceiveNext();
            sendTaskInt = (int)stream.ReceiveNext();
            Debug.Log("Received Task name: " + sendTaskName + " Task ID: " + sendTaskInt);
            SetTaskStatus(sendTaskName, sendTaskInt);
            //pView.RPC("ApplyReceivedChanges", RpcTarget.All, stream);
        }

    }
    public void SetTaskStatus(string name, int id)
    {
        foreach (Task currentTask in tasks)
        { 
            if (currentTask.name == name && currentTask.id == id)
            {
                currentTask.status = TaskStatus.COMPLETED;
            }
        }
    }
    [PunRPC]
    public void ApplyReceivedChanges(PhotonStream stream)
    {
        // Network player, receive data
    
        this.sendTaskName = (string)stream.ReceiveNext();
        this.sendTaskInt = (int)stream.ReceiveNext();
        //this.sendGoingLeft = (bool)stream.ReceiveNext();
        //Debug.Log("Received Task name: " + sendTaskName + " Task ID: " + sendTaskInt + " Bool is: " + sendGoingLeft);
        CheckTasksState(this.sendTaskName, this.sendTaskInt);
    
    }
    #endregion
    private void CheckTasksState(string name, int status)
    {
        Debug.Log("This is the last solved task name: " + name + " and this is the status of the task: " + (TaskStatus)status + "\n" + "This is the value of the bool: ");
    }
}
