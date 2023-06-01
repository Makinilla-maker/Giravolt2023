using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCode : MonoBehaviour
{
    public static int id;
    public bool isAssassin;
    private GameManager gameManager;
    private void Awake()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}
