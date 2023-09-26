using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    [SerializeField] public GameObject Record;
    [SerializeField] public int _numberOfCoins;
    [SerializeField] public int _recordNumberOfCoins;
    [SerializeField] TextMeshProUGUI _text;//чило монет
    [SerializeField] TextMeshProUGUI _recordText;//чило монет рекорда
    public bool flagAddCoins;

    void Start()
    {
        _numberOfCoins = Progress.Instance.PlayerInfo.Coins;
        flagAddCoins = false;
        _recordNumberOfCoins = Progress.Instance.PlayerInfo.RecordOfCoins;
        if (_recordNumberOfCoins > 0)
        {
          Record.SetActive(true);  
        }
        _recordText.text = _recordNumberOfCoins.ToString();
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
    public void VewRecordNumberOfCoins()
    {
        if (_numberOfCoins > _recordNumberOfCoins)
        {
             Record.SetActive(true);
            _recordNumberOfCoins = _numberOfCoins;
            _recordText.text = _recordNumberOfCoins.ToString();
        }
        

    }
}
