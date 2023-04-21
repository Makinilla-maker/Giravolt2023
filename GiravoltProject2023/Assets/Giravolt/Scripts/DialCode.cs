using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
    public class DialCode : MonoBehaviour
    {
    private Rigidbody rb;
    private RigidbodyConstraints originalConstraints;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originalConstraints = rb.constraints;
        FreezeRigidbodyConstraints();
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
            
    }
        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                CheckReleaseAngle();
            }
            Debug.Log(transform.rotation.eulerAngles.y);
        }
        bool IsInRange(float val, float b1, float b2)
        {
            return (val >= Mathf.Min(b1, b2) && val <= Mathf.Max(b1, b2));
        }
     [MenuItem( "Tools/Clear Console %#w" )] // CMD + SHIFT + W
        public void CheckReleaseAngle()
        {
            if (IsInRange(transform.rotation.eulerAngles.y, 0, 22.5f))
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
            }
            else if(IsInRange(transform.rotation.eulerAngles.y, 22.5f, 45f))
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 45f, transform.rotation.z);
            }
        else if (IsInRange(transform.rotation.eulerAngles.y, 45, 67.5f))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 45f, transform.rotation.z);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, 67.5f, 90f))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 90f, transform.rotation.z);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, 90f, 112.5f))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 90f, transform.rotation.z);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, 112.5f, 135f))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 135f, transform.rotation.z);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, 135f, 157.5f))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 135f, transform.rotation.z);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, 157.5f, 180f))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, 180f, 202.5f))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, 202.5f, 225f))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 225f, transform.rotation.z);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, 225f, 247.5f))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 225f, transform.rotation.z);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, 247.5f, 270f))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 270f, transform.rotation.z);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, 270f, 292.5f))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 270f, transform.rotation.z);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, 270f, 315f))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 315f, transform.rotation.z);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, 315f, 337.5f))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 315f, transform.rotation.z);
        }
        else if (IsInRange(transform.rotation.eulerAngles.y, 337.5f, 360f))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
        }
    }
    }


