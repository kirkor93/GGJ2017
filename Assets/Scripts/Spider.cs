using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    [Header("Movement")]
    [Range(0, 10)]
    public int InitialMovementPerEcho;
    [Range(1, 30)]
    public int EchosToImproveMovement;
    [Range(0, 10)]
    public int MovementImproveValue;
    [Range(1, 100)]
    public int TileSize;
    
    private Waypoint _currentWaypoint;

    [Header("Nets")]
    [SerializeField]
    private Net _netPrefab;

    private List<Net> _spawnedNets = new List<Net>();
    

    private bool _initialized;
    private int _currentMovement;
    private int _moveIterations;

    public void Initialize()
    {
        if (_initialized)
        {
            return;
        }
        
        _currentWaypoint = WaypointsManager.Instance.GetClosestWaypoint(transform.position);
        transform.position = (Vector2)_currentWaypoint.transform.position;

        Reset();

        _initialized = true;
    }

    public void Reset()
    {
        _currentMovement = InitialMovementPerEcho;
        _moveIterations = 0;
    }

    public void Move(Transform[] players)
    {
        if (_currentWaypoint == null ||
            (_currentWaypoint.transform.position - transform.position).sqrMagnitude > TileSize)
        {
            Debug.LogError("current waypoint is baaaaaaad :<");
            return;
        }

        TrySpawnNet();

        _currentWaypoint = WaypointsManager.Instance.FindPath(_currentMovement, _currentWaypoint, players, (float)TileSize / 2.0f);
        transform.position = _currentWaypoint.transform.position;

        if ((++_moveIterations % EchosToImproveMovement) == 0)
        {
            _currentMovement += MovementImproveValue;
        }
    }

    private void TrySpawnNet()
    {
        bool alreadySpawnedNet = false;
        foreach (Net net in _spawnedNets)
        {
            if (net != null && 
                ((Vector2)transform.position - (Vector2)net.transform.position).sqrMagnitude < TileSize * TileSize)
            {
                alreadySpawnedNet = true;
                break;
            }
        }
        if (!alreadySpawnedNet)
        {
            GameObject net = Instantiate(_netPrefab.gameObject, transform.position, transform.rotation);
            _spawnedNets.Add(net.GetComponent<Net>());
        }
    }

    private void Awake()
    {
        Initialize();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Bat bat = other.GetComponent<Bat>();
        if (bat == null)
        {
            Debug.Log(string.Format("Spider triggered with {0}. Nothing to do here", other.gameObject.name));
            return;
        }

        GameManager.Instance.StopGame(false);
    }
}
