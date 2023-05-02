using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using Photon;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;
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
    private bool alreadyGeneratedList;
    [SerializeField] private List<Task> allTasks = new List<Task>();
    [SerializeField] public List<Task> tasksForThisGame = new List<Task>();
    public int numberOfTasksForThisGame;
    [SerializeField] private List<int> number = new List<int>();
    private void Awake()
    {
        pView = GetComponent<PhotonView>();
        send = false;
        
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

        if (PhotonNetwork.IsMasterClient)
        {
            pView.RPC("GenerateTasks", RpcTarget.MasterClient);
        }
        
    }
    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (pView.IsMine && !alreadyGeneratedList)
        {
            stream.SendNext(numberOfTasksForThisGame);
            for (int i = 0; i < numberOfTasksForThisGame; ++i)
            {
                int n = tasksForThisGame[i].id;
                stream.SendNext(n);
            }
        }
        else
        {
            int _numberOfTasksForThisGame = (int)stream.ReceiveNext();
            if (tasksForThisGame.Count != _numberOfTasksForThisGame)
            {
                numberOfTasksForThisGame = _numberOfTasksForThisGame;
                for (int i = 0; i < numberOfTasksForThisGame; ++i)
                {
                    int rcvdId = -1;
                    rcvdId = (int)stream.ReceiveNext();
                    for (int k = 0; k < allTasks.Count; ++k)
                    {
                        if (allTasks[k].id == rcvdId)
                        {
                            tasksForThisGame.Add(allTasks[k]);
                        }
                    }
                }
            }
            
            Debug.Log("This is the total number of tasks of this game: " + numberOfTasksForThisGame);
        }
        if (pView.IsMine)
        {
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
    
    public void GenerateTasks()
    {
        if (!alreadyGeneratedList)
        {
            // for (int i = 0; i < numberOfTasksForThisGame; ++i)
            // {
            //     int randomNumber = Random.Range(0, allTasks.Count);
            //     for (int j = 0; j < numberOfTasksForThisGame; ++j)
            //     {
            //         if(allTasks[randomNumber] != tasksForThisGame[j])
            //             tasksForThisGame.Add(allTasks[randomNumber]);
            //     }
            // }
            
            for (int i = 0; i < numberOfTasksForThisGame; ++i)
            {
                int rnd = Random.Range(0, allTasks.Count + 1);
                if (!number.Contains(rnd))
                {
                    tasksForThisGame.Add(allTasks[rnd]);
                    number.Add(rnd);
                }
                else
                {
                    Debug.Log("Duplicated number!");
                }
            }
            
            
            alreadyGeneratedList = true;
        }
    }
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
