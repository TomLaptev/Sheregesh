using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour

{
    [SerializeField] GameObject _bgH;
    [SerializeField] GameObject _bgV;
    bool flagTime = true;

   
    void Update()
    {
        if (flagTime && SceneManager.GetActiveScene().name == "StartScene")
        {
            flagTime = false;

            if (Screen.width / Screen.height >= 16 / 9)
            {
               _bgH.SetActive(true);
               _bgV.SetActive(false);                
            }
            else
            {
               _bgH.SetActive(false);
                _bgV.SetActive(true);
            }

            Invoke("AllowVerification", 1);
        }

      Application.runInBackground = false;
    }

    void AllowVerification()
    {
        flagTime = true;
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
