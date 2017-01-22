using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
	public ParticleSystem deathParticle;
    [Header("Movement")]
    [Range(0, 10)]
    public int InitialMovementPerEcho;
    [Range(1, 30)]
    public int EchosToImproveMovement;
    [Range(0, 10)]
    public int MovementImproveValue;
    [Range(1, 100)]
    public int TileSize;
    [Range(0.0f, 10.0f)]
    public float MovementSpeed = 2.0f;
    
    private Waypoint _currentWaypoint;

    [Header("Nets")]
    [SerializeField]
    private Net _netPrefab;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float _spawnChance = 0.4f;

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
//        transform.position.z = -1.0f;

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
//        if (_currentWaypoint == null ||
//            (_currentWaypoint.transform.position - transform.position).sqrMagnitude > TileSize)
//        {
//            Debug.LogError("current waypoint is baaaaaaad :<");
//            return;
//        }

        TrySpawnNet();

        _currentWaypoint = WaypointsManager.Instance.FindPath(_currentMovement, _currentWaypoint, players, (float)TileSize / 2.0f);
//        transform.position = _currentWaypoint.transform.position;

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
            float randomValue = Random.Range(0.0f, 1.0f);
            if (randomValue < _spawnChance)
            {
                GameObject net = Instantiate(_netPrefab.gameObject, transform.position, transform.rotation);
                _spawnedNets.Add(net.GetComponent<Net>());
            }
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
		GameObject deathPartClone = (GameObject)Instantiate (deathParticle.gameObject, transform.position + Vector3.back, Quaternion.identity);
		print ("boom");
    }

    private void Update()
    {
        Vector2 direction = ((Vector2) _currentWaypoint.transform.position - (Vector2) transform.position);
        Vector2 normalizedDirection = direction.normalized;
        normalizedDirection *= Time.deltaTime * MovementSpeed;
        transform.position = new Vector3(transform.position.x + normalizedDirection.x, transform.position.y + normalizedDirection.y, transform.position.z);

        Vector3 vectorToTarget = _currentWaypoint.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.Euler(0, 0, angle - 90);
        transform.rotation = Quaternion.Lerp(transform.rotation, q, 0.1f);

        if (direction.sqrMagnitude < (TileSize * TileSize) / 2.0f)
        {
            Move(GameManager.Instance.PlayersTransforms);
        }
    }
}
