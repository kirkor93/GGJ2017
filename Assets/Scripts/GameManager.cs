using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private Bat[] _players;

    [SerializeField]
    private Spider[] _spiders;

    private float _timer;

    private bool _initialized;
    private bool _gameRunning;

    public bool IsGameRunning
    {
        get { return _gameRunning; }
    }

    public float GameTimer
    {
        get { return _timer; }
    }

    public void Initialize()
    {
        if (_initialized)
        {
            return;
        }



        _initialized = true;
    }

    public void StartGame()
    {
        if (!_initialized)
        {
            Initialize();
        }

        for (int i = 0; i < _spiders.Length; i++)
        {
            _spiders[i].Reset();
        }

        _gameRunning = true;
    }

    public void RestartGame()
    {
        StopGame(false);
        StartGame();
    }

    public void StopGame(bool isWon)
    {

        _gameRunning = false;
    }

    private void Update()
    {
        if (!_gameRunning)
        {
            return;
        }

        _timer += Time.deltaTime;
    }
}
