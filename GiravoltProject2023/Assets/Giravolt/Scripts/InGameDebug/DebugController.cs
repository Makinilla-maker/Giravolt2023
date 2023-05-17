using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    bool showConsole;

    public void OnToggleDebug()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            showConsole = !showConsole;
        }
    }
}
