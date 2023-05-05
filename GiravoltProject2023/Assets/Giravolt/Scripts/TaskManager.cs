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
    public Task taskCompleted;
    [SerializeField] int ammountOfCompletedTasks = 0;
    private string sendTaskName = "A";
    private int sendTaskInt = -1;
    public bool send = false;
    private PhotonView pView;
    public GameObject ball;
    public int trueNumberOfTasks = 0;
    
    // ISAAC
    private bool alreadyGeneratedList;
    [SerializeField] private List<Task> allTasks = new List<Task>();
    [SerializeField] public List<Task> tasksForThisGame = new List<Task>();
    public int numberOfTasksForThisGame;
    [SerializeField] private List<int> number = new List<int>();
    public string name;

    // place here the info for each created task;
    // DialTask = 0;
    // CremarNota = 1;
    private void Awake()
    {
        pView = GetComponent<PhotonView>();
        send = false;
        
    }
    public void OnPlace(GameObject receptor)
    {
        GameObject go = receptor.transform.GetChild(receptor.transform.childCount - 1).gameObject;

        Debug.Log(go.name);
        foreach(Task currentTask in tasksForThisGame)
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
    public void SetCompletedTask(Task completedTask)
    {
        //completedTask.status = TaskStatus.COMPLETED;
        name = completedTask.name;
        pView.RPC("SendNumberOfCompletedTasks", RpcTarget.All);
    }
    
    private void SendTaskStatus(string task, int status)
    {
        sendTaskName = task;
        sendTaskInt = status;
    }
    private void Update()
    {
        if(ammountOfCompletedTasks == tasksForThisGame.Count)
        {
            //Debug.Log("All tasks are done!");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pView.RequestOwnership();
            send = true;
        }

        if (PhotonNetwork.IsMasterClient && tasksForThisGame.Count != numberOfTasksForThisGame)
        {
            pView.RPC("GenerateTasks", RpcTarget.MasterClient);
        }
        
    }
    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (pView.IsMine)
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
            numberOfTasksForThisGame = _numberOfTasksForThisGame;
            if (tasksForThisGame.Count != _numberOfTasksForThisGame)
            {
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
            //pView.RPC("ApplyReceivedChanges", RpcTarget.All, stream);
        }

    }

    [PunRPC]
    public void SendNumberOfCompletedTasks()
    {
            ammountOfCompletedTasks++;
    }
    [PunRPC]
    public void GenerateTasks()
    {
        if (!alreadyGeneratedList)
        {            
            for (int i = 0; i < numberOfTasksForThisGame; ++i)
            {
                int rnd = Random.Range(0, allTasks.Count + 1);
                if (!number.Contains(rnd))
                {
                    tasksForThisGame.Add(allTasks[rnd]);
                    number.Add(rnd);
                    trueNumberOfTasks++;
                }
                else
                {
                    Debug.Log("Duplicated number!");
                }
                
            }

            numberOfTasksForThisGame = trueNumberOfTasks;
            
            
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
    // this function must be used in the awake function of every gameobject that has a task
    public Task CreateTask(string n, string d, TaskStatus s, GameObject mo, GameObject to, int id)
    {
        for (int i = 0; i < tasksForThisGame.Count; ++i)
        {
            if (tasksForThisGame[i].id == id)
            {
                Task ret = new Task();
                ret.name = n;
                ret.description = d;
                ret.status = s;
                ret.mainObject = mo;
                ret.targetObject = to;
                ret.id = id;
                return ret;
            }
            
        }
        return null;
    }
}
