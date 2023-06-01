using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private GameManager gameManger;
   public void StartBtn()
    {
        gameManger.UpdateGameState(GameState.INGAME);
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
