using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitInput : MonoBehaviour
{
    private CreateRoom r;
    // Start is called before the first frame update
    void Start()
    {
        r = GameObject.Find("CreateRoom").GetComponent<CreateRoom>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            r.LoadSampleScene();
            Debug.Log("Hardcoded load sample scene");
        }
    }
}
