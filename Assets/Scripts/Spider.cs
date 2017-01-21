﻿using System.Collections;
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
    private Waypoint _currentWaypoint;

    [Header("Nets")]
    [SerializeField]
    private Net _netPrefab;

    [Header("Other")]
    [SerializeField]
    private Transform[] _players;
    

    private bool _initialized;
    private int _currentMovement;
    private int _moveIterations;

    public void Initialize()
    {
        if (_initialized)
        {
            return;
        }

        Reset();

        _initialized = true;
    }

    public void Reset()
    {
        _currentMovement = InitialMovementPerEcho;
        _moveIterations = 0;
    }

    public void Move()
    {
        if (_currentWaypoint == null ||
            (_currentWaypoint.transform.position - transform.position).sqrMagnitude > TileSize)
        {
            Debug.LogError("current waypoint is baaaaaaad :<");
            return;
        }

        _currentWaypoint = WaypointsManager.Instance.FindPath(_currentMovement, _currentWaypoint, _players, (float)TileSize / 2.0f);
        transform.position = _currentWaypoint.transform.position;

        if ((++_moveIterations % EchosToImproveMovement) == 0)
        {
            _currentMovement += MovementImproveValue;
        }
    }

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Move();
        }
    }
}
