using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Yandex : MonoBehaviour
{
 [SerializeField] public GameManager GameManager;
    [DllImport("__Internal")]
    private static extern void RateGame(); 
  

    public void RateGameButton()
    {        
       RateGame(); // активировать при билде!!!                 
    }
}
