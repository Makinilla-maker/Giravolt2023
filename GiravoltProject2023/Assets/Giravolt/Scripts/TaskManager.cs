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
    private Task taskCompleted;
    private string sendTaskName = "A";
    private int sendTaskInt = -1;
    private bool send = false;
    private PhotonView pView;
    private int trueNumberOfTasks = 0;
    DialCode d;
    // ISAAC
    private bool alreadyGeneratedList;
    [SerializeField] private List<Task> allTasks = new List<Task>();
    [SerializeField] public List<Task> generatedTasksForThisGame = new List<Task>();
    private List<int> randomNumberList = new List<int>();
    private bool passwordGenerated;
    int ammountOfDialPasswordNumbers;
    // place here the info for each created task;
    // DialTask = 0;
    // CremarNota = 1;
    private void Awake()
    {
        pView = GetComponent<PhotonView>();
        send = false;
        d = GameObject.Find("DialTaskGrab").GetComponent<DialCode>();
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
        if(PhotonNetwork.IsMasterClient)
        {
            if(!passwordGenerated)
            {
                pView.RPC("DialGeneratePassword", RpcTarget.MasterClient);
            }
        }
    }
    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // send the info of the generated tasks 
        if(!alreadyGeneratedList)
        {
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
                            generatedTasksForThisGame.Add(allTasks[k]);
                        }
                    }
                }
                alreadyGeneratedList = true;
            }
        }

        
        if (pView.IsMine)
        {
            if(passwordGenerated)
            {
                stream.SendNext(d.password.Count);
                for (int i = 0; i < d.password.Count; ++i)
                {
                    stream.SendNext(d.password[i]);
                }
            }
            if(send)
            {
                // We own this player: send the others our data
                if (sendTaskName != "")
                {
                    sendTaskName = taskCompleted.name;
                    sendTaskInt = taskCompleted.id;
        
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
            if(!passwordGenerated)
            {
                ammountOfDialPasswordNumbers = (int)stream.ReceiveNext();
                for (int i = 0; i < ammountOfDialPasswordNumbers; ++i)
                {
                    tmpPassword[i] = (int)stream.ReceiveNext();
                }
            }
            pView.RPC("SetCompletedTask", RpcTarget.All);
        }

    }
    public int[] tmpPassword = new int[2];
    public void GetCompletedTask(Task completedTask)
    {
        sendTaskName = completedTask.name;
        sendTaskInt = completedTask.id;
        send = true;
    }
    [PunRPC]
    public void SetCompletedTask()
    {
        for (int i = 0; i < generatedTasksForThisGame.Count; ++i)
        {
            if (generatedTasksForThisGame[i].id == sendTaskInt)
            {
                generatedTasksForThisGame.Remove(generatedTasksForThisGame[i]);
            }
        }
    }
    [PunRPC]
    public void GetDialGeneratedPassword()
    {
        
        for (int i = 0; i < ammountOfDialPasswordNumbers; ++i)
        {
            d.password[i] = tmpPassword[i];
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
    [PunRPC]
    public void DialGeneratePassword()
    {
        
        for (int i = 0; i < 2; ++i)
        {
            d.password.Add(Random.Range(0, 10));
        }
        passwordGenerated = true;
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
}
