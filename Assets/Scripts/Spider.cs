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


    [SerializeField]
    private Transform[] _players;
    [SerializeField]
    private Waypoint _currentWaypoint;

    private bool _initialized;

    private void Initialize()
    {
        if (_initialized)
        {
            return;
        }

        _initialized = true;
    }

    public void Move()
    {
        Vector2 myPosition = transform.position;
        Transform chosenOne = _players[0];
        for (int i = 1; i < _players.Length; i++)
        {
            if((myPosition - (Vector2)_players[i].position).sqrMagnitude < (myPosition - (Vector2)chosenOne.position).sqrMagnitude)
            {
                chosenOne = _players[i];
            }
        }

        

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _currentWaypoint = WaypointsManager.Instance.FindPath(InitialMovementPerEcho, _currentWaypoint, _players, (float)TileSize / 2.0f);
            transform.position = _currentWaypoint.transform.position;
        }
    }
}
