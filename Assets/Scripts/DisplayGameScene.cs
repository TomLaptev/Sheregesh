using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayGameScene : MonoBehaviour
{
    [SerializeField] GameObject _buttonPlay;
    [SerializeField] GameObject _buttonPause;
    bool flagTime = true;
   
   void FixedUpdate()
    {
       if (flagTime)
       {
        flagTime = false;

            if (Screen.width > Screen.height)
        {
            transform.position = new Vector3(5.5f, 3, -5);
             _buttonPlay.transform.position = new Vector3 (Screen.width *0.4f, Screen.height *0.1f, 0);
             _buttonPause.transform.position = new Vector3 (Screen.width *0.4f, Screen.height *0.1f, 0);
        }
        else
        {
            transform.position = new Vector3(5.5f, 3, -2.1f);
            _buttonPlay.transform.position = new Vector3 (Screen.width *0.12f, Screen.width *0.2f, 0);
            _buttonPause.transform.position = new Vector3 (Screen.width *0.12f, Screen.width *0.2f, 0); 
        } 
        Invoke("AllowVerification", 1.5f);
       }
    }
    void AllowVerification()
    {
       flagTime = true; 
    }
}
