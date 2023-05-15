using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;


[System.Serializable]
public class PlayerInfo
{
    public int Coins;
    public int LifesCounter;
}
public class Progress : MonoBehaviour
{
    public PlayerInfo PlayerInfo;
    [SerializeField] GameObject _play;
    [SerializeField] GameObject _continue;
    [SerializeField] GameObject _newGame;

    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);
    [DllImport("__Internal")]
    private static extern void LoadExtern();
    
    public static Progress Instance;

    void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    { 
        //LoadedDates(); // деактивировать при билде!!!
    }

    public void Save()
    {
        string jsonString = JsonUtility.ToJson(PlayerInfo);
        SaveExtern(jsonString); // активировать при билде!!!
    }
    public void SetPlayerInfo(string value)
    {
        PlayerInfo = JsonUtility.FromJson<PlayerInfo>(value);
         LoadedDates();
    }
    public void LoadedDates()
    {
        if (PlayerInfo.Coins + PlayerInfo.LifesCounter > 0)
        {
            _play.SetActive(false);
            _continue.SetActive(true);
            _newGame.SetActive(true);
        }
        else
        {
            _play.SetActive(true);
            _continue.SetActive(false);
            _newGame.SetActive(false);
        }
    }

    public void CollLoadFromHtml()
    {
       LoadExtern(); // активировать при билде!!!
    }

}
