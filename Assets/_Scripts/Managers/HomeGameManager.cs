using UnityEngine;
using System;
using UnityEngine.Advertisements;

public class HomeGameManager : MonoBehaviour
{
    public static HomeGameManager Instance { get; private set; }

    public event EventHandler OnStateChange;


    public enum State
    {
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State _state;
    private float _countdownToStartTimer = 3f;
    private float _gamePlayingTimer;
    private bool _isHouseDestroyed;

    private void Awake()
    {
        Instance = this;
        _state = State.CountdownToStart;
    }

    private void Start()
    {
        //? Disable the banner ad
        BannerAd.Instance.HideBannerAd();
        //* Listen to home health update
        Home.Instance.OnLifeUpdate += Home_OnLifeUpdate;
        OnStateChange?.Invoke(this, EventArgs.Empty);
    }

    private void Update()
    {
        switch (_state)
        {
            case State.CountdownToStart:
                //* Countdown shown at the start of the game
                _countdownToStartTimer -= Time.deltaTime;
                if (_countdownToStartTimer < 0f)
                {
                    //? Countdown has reached 0 here
                    _state = State.GamePlaying;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                    _gamePlayingTimer = 0f;
                }
                break;
            case State.GamePlaying:
                //* Game is playing here
                //? Icrease the game session time to keep track of player max time
                _gamePlayingTimer += Time.deltaTime;
                if (_isHouseDestroyed)
                {
                    //? House is destroyed here
                    _state = State.GameOver;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                //* Game over here
                break;
        }
    }

    private void Home_OnLifeUpdate(object sender, Home.OnLifeUpdateEventArgs e)
    {
        if (e.life <= 0)
            _isHouseDestroyed = true;
    }

    public State GetCurrentState() => _state;

    public float GetCountdownToStartTimer() => _countdownToStartTimer;

}
