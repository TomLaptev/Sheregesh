using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    [SerializeField] public int _numberOfCoins;
    [SerializeField] TextMeshProUGUI _text;//чило монет
    public bool flagAddCoins;

   // public static CoinManager Instance;
    // void Awake()
    // {
    //     if (Instance == null)
    //     {
    //         transform.parent = null;
    //         DontDestroyOnLoad(gameObject);
    //         Instance = this;
    //     }
    //     else        
    //         Destroy(gameObject);        
    // }

    void Start()
    {
        _numberOfCoins = Progress.Instance.PlayerInfo.Coins;
        flagAddCoins = false;
        VewNumberOfCoins();
    }
    public void AddOne()
    {
        _numberOfCoins++;
        flagAddCoins = true;
        _text.text = _numberOfCoins.ToString();
    }

    public void VewNumberOfCoins()
    {
        _text.text = _numberOfCoins.ToString();
    }
}
