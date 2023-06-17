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
    private string sendTaskStatus = "A";
    private int sendTaskInt = -1;
    private bool send = false;
    public bool sendWipeTask = false;
    private PhotonView pView;
    private int trueNumberOfTasks = 0;

    public GameObject sangMaterial;
    public GameObject tacaMaterial;

    // ISAAC
    private bool alreadyGeneratedList;
    public int ammountOfWipesTaques = 3;
    public int ammountOfWipesSang = 3;
    [SerializeField] private List<Task> allTasks = new List<Task>();
    public List<Task> generatedTasksForThisGame = new List<Task>();
    private List<int> randomNumberList = new List<int>();
    public int numberOfTasksPerGame = 10;
    public GameObject gameLightsHolder;
    private LightsSwitch ls;
    // place here the info for each created task;
    // DialTask = 0;
    // Placement Tasks = ++4;
    private void Awake()
    {
        pView = GetComponent<PhotonView>();
        send = false;
        if (pView) pView.ObservedComponents.Add(this);

        gameLightsHolder = GameObject.Find("InGameLights");
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
        if (IsAnyTaskLeft())
        {
            //Debug.Log("HOLA ORIOL, ETS UN MONGOLIN LOQUETE!\n\ngilipollas");
        }
        else
        {
            //Debug.Log("ORIOL TREBALLA BOBO");
        }
    }
    public bool IsAnyTaskLeft()
    {
        bool ret = false;
        if (generatedTasksForThisGame.Count == 0)
        {
            ret = true;
        }
        else
        {
            ret = false;
        }

        return ret;
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
                            if (allTasks[k].status != TaskStatus.COMPLETED)
                                generatedTasksForThisGame.Add(allTasks[k]);
                        }

                    }
                }
            }
            alreadyGeneratedList = true;
            HideUnselectedTasks();
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
            var check = " ";
            check = (string)stream.ReceiveNext();
            Debug.Log("DADSASDAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA BAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD CAAAAAAAAAAAAAST " + (string)stream.ReceiveNext());
            if (check != " ")
            {
                sendTaskStatus = check;
                sendTaskInt = (int)stream.ReceiveNext();
            

            }
        }

    }
    public void GetCompletedTask(Task completedTask)
    {
        taskCompleted = completedTask;
        //send = true;
        pView.RPC("SetCompletedTask", RpcTarget.All, (string)taskCompleted.name);
    }
    [PunRPC]
    public void DecreaseWipe(int id)
    {
        switch(id)
        {
            case 13:
                sangMaterial.GetComponent<MeshRenderer>().material.color = new Color(sangMaterial.GetComponent<MeshRenderer>().material.color.r, sangMaterial.GetComponent<MeshRenderer>().material.color.g, sangMaterial.GetComponent<MeshRenderer>().material.color.b, (float)(0.33 * ammountOfWipesSang));
                ammountOfWipesSang--;
                break;
            case 8:
                tacaMaterial.GetComponent<MeshRenderer>().material.color = new Color(tacaMaterial.GetComponent<MeshRenderer>().material.color.r, tacaMaterial.GetComponent<MeshRenderer>().material.color.g, tacaMaterial.GetComponent<MeshRenderer>().material.color.b, (float)(0.33 * ammountOfWipesSang));
                ammountOfWipesTaques--;
                break;
            default: break;
        }
    }
    [PunRPC]
    public void SetCompletedTask(string tn)
    {
        for (int i = 0; i < generatedTasksForThisGame.Count; ++i)
        {
            if ((string)generatedTasksForThisGame[i].name == tn)
            {
                trueNumberOfTasks--;
                generatedTasksForThisGame[i].status = TaskStatus.COMPLETED;
                OnceTaskComplete(generatedTasksForThisGame[i].id);
                generatedTasksForThisGame.Remove(generatedTasksForThisGame[i]);
            }
        }
        
    }
    
    public void CallTurnLights(bool b, string name)
    {
        pView.RPC("TurnLihtsOnOrOff", RpcTarget.All, b, name);
    }
    [PunRPC]
    public void TurnLihtsOnOrOff(bool b, string name)
    {
        foreach (Transform g in gameLightsHolder.transform.GetComponentInChildren<Transform>())
        {
            switch (b)
            {
                case true:
                    g.gameObject.SetActive(b);
                    GameObject.Find(name).GetComponent<LightsSwitch>().ChangeCubeColor(Color.green);
                    break;
                case false:
                    g.gameObject.SetActive(b);
                    GameObject.Find(name).GetComponent<LightsSwitch>().ChangeCubeColor(Color.red);
                    break;
                default:
                    break;
            }
        }
    }
    [PunRPC]
    public void GenerateTasks()
    {
        if (!alreadyGeneratedList)
        {
            for (int i = 0; i < numberOfTasksPerGame; ++i)
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
            HideUnselectedTasks();
            alreadyGeneratedList = true;
        }
    }
    public void HideUnselectedTasks()
    {
        foreach (Task t in allTasks)
        {
            if(!generatedTasksForThisGame.Contains(t))
            {
                Debug.Log("About to deactivate: " + t.name + "_Main");
                if(GameObject.Find(t.name + "_Main"))
                    GameObject.Find(t.name + "_Main").SetActive(false);
            }
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
        GameObject go;
        switch (id)
        {
            case 0:
            // DIAL
                
                go = GameObject.Find("DialTaskGrab");
                go.GetComponent<DialCode>().OnCompletedTask();
                break;
            case 4:
                go = GameObject.Find("Task_Factura");
                go.GetComponent<PlacementTasks>().OnCompletedTask();
                break;
            case 5:
                go = GameObject.Find("Task_Amagar");
                go.GetComponent<PlacementTasks>().OnCompletedTask();
                break;
            case 6:
                go = GameObject.Find("Task_Llibre");
                go.GetComponent<PlacementTasks>().OnCompletedTask();
                break;
            case 7:
                go = GameObject.Find("Task_Caixa");
                go.GetComponent<PlacementTasks>().OnCompletedTask();
                break;
            case 8:
                go = GameObject.Find("Task_Taques");
                go.GetComponent<PlacementTasks>().OnCompletedTask();
                break;
            case 9:
                go = GameObject.Find("Task_Informacio");
                go.GetComponent<PlacementTasks>().OnCompletedTask();
                break;
            case 10:
                go = GameObject.Find("Task_Escombraries");
                go.GetComponent<PlacementTasks>().OnCompletedTask();
                break;
            case 11:
                go = GameObject.Find("Task_Porta");
                go.GetComponent<PlacementTasks>().OnCompletedTask();
                break;
            case 12:
                go = GameObject.Find("Task_Fuet");
                go.GetComponent<PlacementTasks>().OnCompletedTask();
                break;
            case 13:
                go = GameObject.Find("Task_Taca");
                go.GetComponent<PlacementTasks>().OnCompletedTask();
                break;
            case 14:
                go = GameObject.Find("Task_Cos");
                go.GetComponent<PlacementTasks>().OnCompletedTask();
                break;
            case 15:
                go = GameObject.Find("Task_Sang");
                go.GetComponent<PlacementTasks>().OnCompletedTask();
                break;
            case 16:
                go = GameObject.Find("Task_Vaixell");
                go.GetComponent<PlacementTasks>().OnCompletedTask();
                break;
            case 17:
                go = GameObject.Find("Task_Gerro");
                go.GetComponent<PlacementTasks>().OnCompletedTask();
                break;
            case 18:
                go = GameObject.Find("Task_Espelma");
                go.GetComponent<PlacementTasks>().OnCompletedTask();
                break;
            default:
                break;
        }
        if(generatedTasksForThisGame.Count == 0)
        {
            pView.RPC("EndGame", RpcTarget.All);
        }
    }

}
