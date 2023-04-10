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

public class TaskManager : MonoBehaviour
{
    public List<Task> tasks = new List<Task>();
    public GameObject go;
    int taskCompleted = 0;
    private PhotonView pView;
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
                if(task.status == TaskStatus.COMPLETED && !pView.IsMine)
                {
                    Instantiate(go, receptor.transform.position, go.transform.rotation);
                }
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
