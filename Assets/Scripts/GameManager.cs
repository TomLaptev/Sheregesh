using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] RoadGenerator RoadGenerator;
    [SerializeField] ObjectsGenerator ObjectsGenerator;
    [SerializeField] PlayerController PlayerController;
    [SerializeField] CoinManager CoinManager;
    [SerializeField] public GameObject _play;
    [SerializeField] public GameObject _pause;
    [SerializeField] GameObject _continue;
    [SerializeField] GameObject _startNewGame;
    [SerializeField] GameObject _cancel;
    [SerializeField] GameObject _rateGame;
    [SerializeField] GameObject _gameOver;
    [SerializeField] public GameObject _areWePlaying;
    [SerializeField] public GameObject[] _lifes;
    [SerializeField] Animator _animator;
    [SerializeField] TextMeshProUGUI _levelNumber;
    [SerializeField] TextMeshProUGUI _levelString;
    [SerializeField] public AudioSource _soundTheme;
    [SerializeField] public AudioSource _soundBang;
    [SerializeField] public AudioSource _soundAdvance;


    [DllImport("__Internal")]
    static extern void SetToLeaderboard(int value);
    [DllImport("__Internal")]
    static extern void ShowAdv();
    [DllImport("__Internal")]
    static extern void ContinueForCoinsExtern();

    bool flagContinue = false;
    bool flagStand = false;
    bool flagConRepeatColision = true;
    bool flagPause = true;
    bool flagSpace = true;
    public bool isMusic;

    int StartMinNumberCoins = 25;
    int CurrentMinNumberCoins;
    int NumberLevel;
    int StartNumberCoinsForGrowLevel = 100;
    int CurrentNumberCoinsForGrowLevel;
    int CounterForLifes;



    void Start()
    {
       ShowAdv(); // активировать при билде!!!
        CounterForLifes = Progress.Instance.PlayerInfo.LifesCounter;
        CountLevel();
    }


    public void AdvertisementShown()
    {
        _soundTheme.Play();
        isMusic = true; 
    }

    void FixedUpdate()
    {
        if (flagSpace && Input.GetKey(KeyCode.Space))
        {
            flagSpace = false;
            Invoke("AllowClickSpace", 0.5f);
            if (flagPause || !flagStand)
            {
                Go();
            }
            else
            {
                Pause();
            }

        }
        if (CoinManager._numberOfCoins >= CurrentNumberCoinsForGrowLevel)
        {
            CountLevel();
            CurrentNumberCoinsForGrowLevel += StartNumberCoinsForGrowLevel;

            //Минимальное число монет для уровня
            CurrentMinNumberCoins = StartMinNumberCoins + StartNumberCoinsForGrowLevel * (NumberLevel - 1);
            Invoke("UpdateLevel", 0.1f);
        }
    }
    void AllowClickSpace()
    {
        flagSpace = true;
    }

    void CountLevel()
    {
        if (NumberLevel < 10)
        {
            NumberLevel = (int)Mathf.Floor(CoinManager._numberOfCoins / StartNumberCoinsForGrowLevel) + 1;
        }
        else
        {
            NumberLevel = 10;
        }
    }
    void UpdateLevel()
    {
        Pause();
        Reset();
        if (CoinManager.flagAddCoins && CoinManager._numberOfCoins < 1003)
        {
            CounterForLifes = 0;
        }
        InstallLifes();
        VewNumberOfLevel();
        flagContinue = false;
        RoadGenerator.maxSpeed += 1.2f;
        if (NumberLevel != 1 && CoinManager.flagAddCoins)
        {
            _animator.SetTrigger("Victory Idle");
            _animator.SetBool("Standing Idle", true);
            _soundTheme.Stop();
            isMusic = false;
            _soundAdvance.Play();

            Invoke("ShowAdv", 2.5f); // активировать при билде!!!
        }

        SaveToProgress();
    }

    void InstallLifes()
    {
        for (int i = 0; i < CounterForLifes; i++)
        {
            _lifes[_lifes.Length - 1 - i].SetActive(false);
        }
    }

    public void VewNumberOfLevel()
    {
        _levelNumber.text = NumberLevel.ToString();
    }

    public void Go()
    {
         if (!isMusic)
        {
            _soundTheme.Play();
            isMusic = true;
        }
        _play.SetActive(false);
        _pause.SetActive(true);
        RoadGenerator.StartLevel();
        flagPause = false;
        flagStand = true;
        flagConRepeatColision = true;
    }
    public void Pause()
    {
        RoadGenerator.currentSpeed = 0;
        _pause.SetActive(false);
        _play.SetActive(true);
        flagPause = true;
        SaveToProgress();
    }
    public void Continue()
    {
        CoinManager._numberOfCoins -= StartMinNumberCoins;
        CoinManager.VewNumberOfCoins();
        ContinueGame();

    }

    public void Cancel()
    {
        ExitTheGame();
    }

    public void NewGame()
    {
        Progress.Instance.PlayerInfo.LifesCounter = 0;
        Progress.Instance.PlayerInfo.Coins = 0;
        _startNewGame.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void ContinueButton()
    {
      ContinueForCoinsExtern(); // активировать при билде!!!       
    }

    public void DecrementLifesPlayer()
    { 
        flagSpace = true; 
        _soundBang.Play();

        if (CounterForLifes < _lifes.Length && flagConRepeatColision)
        {
            flagConRepeatColision = false;
            _animator.SetBool("Standing Idle", true);
            _lifes[_lifes.Length - 1 - CounterForLifes].SetActive(false);
            CounterForLifes++;
            Pause();
        }

        if (CounterForLifes >= _lifes.Length)
        {
            flagSpace = false;
            flagPause = false;
            flagStand = true;
            PlayerController.hideControl();
            _animator.SetBool("Standing Idle", false);
            ObjectsGenerator.ResetObjects();
            RoadGenerator.ResetLevel();
            _play.SetActive(false);
            _pause.SetActive(false);
            _areWePlaying.SetActive(true);

            if (CoinManager._numberOfCoins >= CurrentMinNumberCoins && CoinManager._numberOfCoins < 1000)
            {
                _continue.SetActive(true);//после down -> ContinueButton() -> adv -> Continue() -> ContinueGame();
                _cancel.SetActive(true);
            }

            else ExitTheGame();

            SaveNullProgress();
        }

        if (flagContinue)
        {
            foreach (var item in _lifes)
            {
                item.SetActive(false);
            }
            flagContinue = false;
        }
    }

    public void ContinueGame()
    {
        flagSpace = true;
        flagContinue = true;
        flagStand = false;
        _continue.SetActive(false);
        _gameOver.SetActive(false);
        _lifes[0].SetActive(true);
        CounterForLifes = _lifes.Length - 1;
        _animator.SetBool("Standing Idle", true);
        _cancel.SetActive(false);
        _areWePlaying.SetActive(false);
        _play.SetActive(true);
        PlayerController.showControl();
        SaveToProgress();
    }

    public void ExitTheGame()
    {
        ShowAdv(); // активировать при билде!!!
        _continue.SetActive(false);
        _cancel.SetActive(false);
        _gameOver.SetActive(true);
        _areWePlaying.SetActive(false);
        _rateGame.SetActive(true);
        _startNewGame.SetActive(true);
        if (NumberLevel > 2)
        {
            _animator.SetBool("Victory Idle 0", true);
        }
        SetToLeaderboard(CoinManager._numberOfCoins); // активировать при билде!!!
    }

    public void PermissionRating()
    {
        _rateGame.SetActive(false);
    }

    public void HideRatingButton()
    {
        _rateGame.SetActive(false);
    }
    public void SaveNullProgress()
    {
        Progress.Instance.PlayerInfo.LifesCounter = 0;
        Progress.Instance.PlayerInfo.Coins = 0;
        Progress.Instance.Save();
    }
    public void SaveToProgress()
    {
        Progress.Instance.PlayerInfo.Coins = CoinManager._numberOfCoins;
        Progress.Instance.PlayerInfo.LifesCounter = CounterForLifes;
        Progress.Instance.Save(); //сохраняем данные
    }

    void Reset()
    {
        _pause.SetActive(false);
        _gameOver.SetActive(false);
        _continue.SetActive(false);
        _cancel.SetActive(false);
        _rateGame.SetActive(false);
        _areWePlaying.SetActive(false);

        if (CoinManager._numberOfCoins <= 1002)
        {
            foreach (var item in _lifes)
            {
                item.SetActive(true);
            }

        }
    }
}