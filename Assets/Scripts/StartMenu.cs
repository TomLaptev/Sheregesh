using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour

{
    void FixedUpdate()
    {

      Application.runInBackground = false;
    }   

    public void Play()
    {
        Progress.Instance.PlayerInfo.LifesCounter = 0;
        Progress.Instance.PlayerInfo.Coins = 0;
        SceneManager.LoadScene(1);
    }
    public void Continue()
    {
        SceneManager.LoadScene(1);
    }

}
