using Autohand.Demo;
using Autohand;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Kinfe : MonoBehaviour
{
    public LayerMask stabbableLayers;
    [SerializeField] public bool isStabbing;
    [SerializeField] private float speed;
    [SerializeField] public GameObject knife;

    void Start()
    {
        isStabbing = false;

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CalculateSpeed());

        
    }

    IEnumerator CalculateSpeed()
    {
        Vector3 lastPos = transform.position;
        yield return new WaitForFixedUpdate();
        speed = (lastPos- transform.position).magnitude / Time.deltaTime;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (isStabbing == false && speed >= 1.5f && collision.gameObject.tag == "Player")
        {
            isStabbing = true;
            collision.gameObject.GetComponent<DeathManager>().isAlive = false;
        }
            else isStabbing = false;
    }
}
