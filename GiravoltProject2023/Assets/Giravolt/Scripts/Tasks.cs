using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task
{
    public string name;
    public string description;
    public TaskStatus status;
    public GameObject mainObject;
    public GameObject targetObject;
    public int id;
}
