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
using System.IO;

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
    [SerializeField]private Task taskCompleted;
    [SerializeField] private string sendTaskStatus = "A";
    private int sendTaskInt = -1;
    private bool send = false;
    private PhotonView pView;
    private int trueNumberOfTasks = 0;
    // ISAAC
    private bool alreadyGeneratedList;
    [SerializeField] private List<Task> allTasks = new List<Task>();
    [SerializeField] public List<Task> generatedTasksForThisGame = new List<Task>();
    private List<int> randomNumberList = new List<int>();
    // place here the info for each created task;
    // DialTask = 0;
    // CremarNota = 1;
    private void Awake()
    {
        pView = GetComponent<PhotonView>();
        send = false;
        if (pView) pView.ObservedComponents.Add(this);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pView.RequestOwnership();
            send = true;
        }
        if (PhotonNetwork.IsMasterClient && !alreadyGeneratedList)
        {
            pView.RPC("GenerateTasks", RpcTarget.MasterClient);
            
        }
    }
    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // send the info of the generated tasks 
        
            if (pView.IsMine)
            {
                stream.SendNext(trueNumberOfTasks);
                for (int i = 0; i < trueNumberOfTasks; ++i)
                {
                    int n = generatedTasksForThisGame[i].id;
                    stream.SendNext(n);
                }
            }
            else
            {
                trueNumberOfTasks = (int)stream.ReceiveNext();
                int rcvdId = -1;
                for (int i = 0; i < trueNumberOfTasks; ++i)
                {
                    rcvdId = (int)stream.ReceiveNext();
                    for (int k = 0; k < allTasks.Count; ++k)
                    {
                        if (allTasks[k].id == rcvdId)
                        {

                            if (!generatedTasksForThisGame.Contains(allTasks[k]))
                            {
                                if(allTasks[k].status != TaskStatus.COMPLETED)
                                    generatedTasksForThisGame.Add(allTasks[k]);
                            }

                        }
                    }
                }
                alreadyGeneratedList = true;
            }
        
            
        

        
        if (pView.IsMine)
        {
            if(send)
            {
                // We own this player: send the others our data
                if (taskCompleted.name != "")
                {
                    sendTaskStatus = taskCompleted.status.ToString();
                    sendTaskInt = taskCompleted.id;
        
                    stream.SendNext(sendTaskStatus);
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
            string check = (string)stream.ReceiveNext();
            if (check != "")
            {
                sendTaskStatus = check;
                sendTaskInt = (int)stream.ReceiveNext();
            
                pView.RPC("SetCompletedTask", RpcTarget.All);

            }
        }

    }

    public void GetCompletedTask(Task completedTask)
    {
        Debug.Log("Get completed Task funtion");
        taskCompleted = completedTask;
        send = true;
    }
    [PunRPC]
    public void SetCompletedTask()
    {
        for (int i = 0; i < generatedTasksForThisGame.Count; ++i)
        {
            if (generatedTasksForThisGame[i].id == sendTaskInt)
            {
                trueNumberOfTasks--;
                generatedTasksForThisGame[i].status = TaskStatus.COMPLETED;
                OnceTaskComplete(generatedTasksForThisGame[i].id);
                generatedTasksForThisGame.Remove(generatedTasksForThisGame[i]);
            }
        }
        
    }
    [PunRPC]
    public void GenerateTasks()
    {
        if (!alreadyGeneratedList)
        {
            for (int i = 0; i < allTasks.Count; ++i)
            {
                int rnd = Random.Range(0, allTasks.Count);
                //tasksForThisGame.Add(allTasks[i]);
                if (!randomNumberList.Contains(rnd))
                {
                    generatedTasksForThisGame.Add(allTasks[rnd]);
                    randomNumberList.Add(rnd);
                    trueNumberOfTasks++;
                }
                else
                {
                }
                
            }
            alreadyGeneratedList = true;
        }
    }
    
    #endregion
    // this function must be used in the awake function of every gameobject that has a task
    public Task CreateTask(string n, string d, TaskStatus s, GameObject mo, GameObject to, int id)
    {
        for (int i = 0; i < generatedTasksForThisGame.Count; ++i)
        {
            if (generatedTasksForThisGame[i].id == id)
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
    public void OnceTaskComplete(int id)
    {
        switch (id)
        {
            case 0:
                GameObject go;
                go = GameObject.Find("Dial");
                go.GetComponent<DialCode>().OnCompletedTask();
                break;
            default:
                break;
        }
    }

}
