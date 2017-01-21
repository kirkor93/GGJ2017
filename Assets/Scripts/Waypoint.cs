using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private Waypoint[] _neighbours = new Waypoint[4];

    private const float SearchCircleRadiusMin = 0.1f;
    private const float SearchCircleRadiusMax = 1.1f;

    private const float SqrSearchRadiusMax = SearchCircleRadiusMax * SearchCircleRadiusMax;
    private const float SqrSearchRadiusMin = SearchCircleRadiusMin * SearchCircleRadiusMin;

    public Waypoint UpperNeighbour { get { return _neighbours[0]; } private set { _neighbours[0] = value; } }
    public Waypoint LowerNeighbour { get { return _neighbours[1]; } private set { _neighbours[1] = value; } }
    public Waypoint LeftNeighbour { get { return _neighbours[2]; } private set { _neighbours[2] = value; } }
    public Waypoint RightNeighbour { get { return _neighbours[3]; } private set { _neighbours[3] = value; } }
    public Waypoint[] Neighbours { get { return _neighbours; }}

    public void Initialize()
    {
        for (int i = 0; i < _neighbours.Length; i++)
        {
            _neighbours[i] = null;
        }

        Waypoint[] allPossible = transform.parent.GetComponentsInChildren<Waypoint>(true);
        List<Waypoint> neighbours = new List<Waypoint>();

        foreach (Waypoint waypoint in allPossible)
        {
            float distance = ((Vector2) waypoint.transform.position - (Vector2) transform.position).sqrMagnitude;
            if (distance > SqrSearchRadiusMin && 
                distance < SqrSearchRadiusMax)
            {
                neighbours.Add(waypoint);
            }
        }

        Vector2[] directions = new[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        for (int i = 0; i < neighbours.Count; i++)
        {
            for (int j = 0; j < directions.Length; j++)
            {
                if (Vector2.Angle(neighbours[i].transform.position - transform.position, directions[j]) < 45.0f)
                {
                    _neighbours[j] = neighbours[i];
                }
            }
        }
    }
}
