using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
    public class DialCode : MonoBehaviour
    {
    private Rigidbody rb;
    private RigidbodyConstraints originalConstraints;
    [SerializeField] private List<int> password = new List<int>();
    [SerializeField] private List<int> userInputPassword = new List<int>();
    [SerializeField] private int divisions = 10;
    [SerializeField] private int divisionsAngle;
    [SerializeField] private int d;
    private TaskManager manager;

    // this code is for this script only and will only be used if this task is added to tasksForThisGame list
    Task dialTask = new Task();

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originalConstraints = rb.constraints;
        FreezeRigidbodyConstraints();
        divisionsAngle = 360 / divisions;
        d = divisionsAngle / 2;
        manager = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        // we go to the task manager and generate thas task and assign its info
        
    }

    public void FreezeRigidbodyConstraints()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    public void UnFreezeRigidboydConstraints()
    {
        rb.constraints = originalConstraints;
    }
    private void Start()
    {
        dialTask = manager.CreateTask("DialTask", "This task is releated to password", TaskStatus.NOTSTARTED, null, null, 0);
        Debug.Log("Name of the status = " + dialTask.status);
        for (int i = 0; i < 2; ++i)
        {
            password.Add(Random.Range(0, 10));
        }
    }
        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                CheckReleaseAngle();
            }
        }
        bool IsInRange(float val, float b1, float b2)
        {
            return (val >= Mathf.Min(b1, b2) && val <= Mathf.Max(b1, b2));
        }
        public void CheckReleaseAngle()
        {
        // ------------------- NUMBERS -------------------
        if (IsInRange(transform.rotation.eulerAngles.y, 0, d))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
            userInputPassword.Add(0);
        }
        else if(IsInRange(transform.rotation.eulerAngles.y, d, d*2))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, divisionsAngle, transform.rotation.z);
            userInputPassword.Add(1);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, d*2, d*3))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, divisionsAngle, transform.rotation.z);
            userInputPassword.Add(1);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, d*3, d*4))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, divisionsAngle*2, transform.rotation.z);
            userInputPassword.Add(2);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, d*4, d*5))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, divisionsAngle*2, transform.rotation.z);
            userInputPassword.Add(2);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, d*5, d*6))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, divisionsAngle*3, transform.rotation.z);
            userInputPassword.Add(3);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, d*6, d*7))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, divisionsAngle*3, transform.rotation.z);
            userInputPassword.Add(3);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, d*7, d*8))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, divisionsAngle*4, transform.rotation.z);
            userInputPassword.Add(4);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, d*8, d*9))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, divisionsAngle*4, transform.rotation.z);
            userInputPassword.Add(4);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, d*9, d*10))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, divisionsAngle*5, transform.rotation.z);
            userInputPassword.Add(5);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, d*10, d*11))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, divisionsAngle*5, transform.rotation.z);
            userInputPassword.Add(5);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, d*11, d*12))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, divisionsAngle*6, transform.rotation.z);
            userInputPassword.Add(6);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, d*12, d*13))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, divisionsAngle*6, transform.rotation.z);
            userInputPassword.Add(6);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, d*13, d*14))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, divisionsAngle*7, transform.rotation.z);
            userInputPassword.Add(7);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, d*14, d*15))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, divisionsAngle*7, transform.rotation.z);
            userInputPassword.Add(7);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, d*15, d*16))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, divisionsAngle*8, transform.rotation.z);
            userInputPassword.Add(8);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, d*16, d*17))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, divisionsAngle*8, transform.rotation.z);
            userInputPassword.Add(8);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, d*17, d*18))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, divisionsAngle*9, transform.rotation.z);
            userInputPassword.Add(9);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, d * 18, d * 19))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
            userInputPassword.Add(9);
        }
        if(CheckCorrectPassword())
        {
            Debug.Log("Correct Password");
            manager.SetCompletedTask(dialTask);
            Debug.Log(dialTask.status.ToString() + "status of the task");
        }
        else
        {
            Debug.Log("Incorrect password");
        }
    }
    bool CheckCorrectPassword()
    {
        return password.SequenceEqual(userInputPassword);
    }
}


