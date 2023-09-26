using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public class PlayerInfo
{
    public int Coins;
    public int RecordOfCoins;
    public int LifesCounter;
}
public class Progress : MonoBehaviour
{

    public PlayerInfo PlayerInfo;
    [SerializeField] GameObject _play;
    [SerializeField] GameObject _continue;
    [SerializeField] GameObject _newGame;
    [SerializeField] GameObject _buttonsControl;
    public bool isDesktop;
    public bool isAuth;

    [DllImport("__Internal")]
    private static extern void LoadExtern();

    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);

    /**/
    [DllImport("__Internal")]
    private static extern void GetData(string name);

    [DllImport("__Internal")]
    private static extern void SetData(string name, string info);


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
        //  LoadedDates(); // деактивировать при билде!!!
        //  isAuth = true;    // деактивировать при билде!!!
        //  isDesktop = true; // деактивировать при билде!!!

        {
            if (isDesktop)
            {
                _buttonsControl.SetActive(true);
            }
            else
            {
                _buttonsControl.SetActive(false);
            }
        }
    }

    public void InitLoader_Auth()
    {
        isAuth = true;
        LoadExtern();
    }

    public void InitLoader_NotAuth()
    {
        GetData("DatasKey");
    }

    public void IsDesktop()
    {
        isDesktop = true;
    }
    public void Save()
    {
        if (isAuth)
        {
            string jsonString = JsonUtility.ToJson(PlayerInfo);
            SaveExtern(jsonString); // активировать при билде!!!            
        }
        else
        {
            string datas = JsonUtility.ToJson(PlayerInfo);
            SetData("DatasKey", datas); // активировать при билде!!! ;
        }
    }

    public void SetPlayerInfo(string value)
    {
        PlayerInfo = JsonUtility.FromJson<PlayerInfo>(value);
        LoadedDates();
    }
    public void SetPlayerZeroInfo()
    {
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
}
