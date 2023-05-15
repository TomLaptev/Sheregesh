using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Go : MonoBehaviour
 
{
    [SerializeField] GameObject _img_H;
    [SerializeField] GameObject _img_V;
    [SerializeField] GameObject _buttonsControl;
    bool flagTime = true; 

       void Update()
    {
        if (flagTime && SceneManager.GetActiveScene().name == "SettingsScene")
        {
            flagTime = false;

            if (Screen.width / Screen.height >= 16/9 )
            {
                _img_H.SetActive(true);
               _img_V.SetActive(false);
                _buttonsControl.SetActive(true);                 
            }
            else
            {
               _img_H.SetActive(false);
                _img_V.SetActive(true);
                _buttonsControl.SetActive(false);  
            }
            Invoke("AllowVerification", 1);
        }
    }

    void AllowVerification()
    {
        flagTime = true;
    }  

    public void Continue()
    {
        SceneManager.LoadScene(2);

    }
}
