using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaypointsManager))]
public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private Bat[] _players;

    [SerializeField]
    private Spider[] _spiders;

    [SerializeField]
    private Fly[] _fliesPrefabs;

    [SerializeField]
    [Range(0, 30)]
    private int _fliesSpawnedPerEcho;

    private float _timer;

    private bool _initialized;
    private bool _gameRunning;
    private Transform[] _playersTransforms;
    private WaypointsManager _waypointsManager;

    private List<Fly> _spawnedFlies = new List<Fly>();

    public bool IsGameRunning
    {
        get { return _gameRunning; }
    }

    public float GameTimer
    {
        get { return _timer; }
    }

    public Transform[] PlayersTransforms
    {
        get { return _playersTransforms; }
    }

    public Bat[] Players
    {
        get { return _players; }
    }

    public void Initialize()
    {
        if (_initialized)
        {
            return;
        }

        _waypointsManager = GetComponent<WaypointsManager>();
        _playersTransforms = new Transform[_players.Length];
        for (int i = 0; i < _players.Length; i++)
        {
            _playersTransforms[i] = _players[i].GetComponent<Transform>();
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

    private void Awake()
    {
        StartGame();
    }

    private void Update()
    {
        if (!_gameRunning)
        {
            return;
        }

        _timer += Time.deltaTime;
    }

//    public void MoveSpider()
//    {
//        for (int i = 0; i < _spiders.Length; i++)
//        {
//            _spiders[i].Move(_playersTransforms);
//        }
//    }

    public void ProcessFlies()
    {
        foreach (Fly fly in _spawnedFlies)
        {
            if (fly != null)
            {
                Destroy(fly.gameObject);
            }
        }
        _spawnedFlies.Clear();

        HashSet<Waypoint> usedWaypoints = new HashSet<Waypoint>();
        for (int i = 0; i < _fliesSpawnedPerEcho; i++)
        {
            Waypoint[] possibleWaypoints = _waypointsManager.AvailableWaypoints;
            Waypoint selected = possibleWaypoints[Random.Range(0, possibleWaypoints.Length)];
            if (!usedWaypoints.Contains(selected))
            {
                usedWaypoints.Add(selected);
                GameObject fly = Instantiate(_fliesPrefabs[Random.Range(0, _fliesPrefabs.Length)].gameObject,
                    new Vector3(selected.transform.position.x, selected.transform.position.y, -0.01f), Quaternion.identity);
                _spawnedFlies.Add(fly.GetComponent<Fly>());
            }
        }
    }
}
