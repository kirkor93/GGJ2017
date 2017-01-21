using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsManager : Singleton<WaypointsManager>
{
    private Waypoint[] _availableWaypoints;

    public void Initialize()
    {
        _availableWaypoints = GetComponentsInChildren<Waypoint>(true);
        foreach (Waypoint waypoint in _availableWaypoints)
        {
            waypoint.Initialize();
        }
    }

    private void Awake()
    {
        Initialize();
    }

    private float CountHeuristics(Waypoint waypoint, Vector2 playerPosition)
    {
        return ((Vector2) waypoint.transform.position - playerPosition).sqrMagnitude;
    }

    public Waypoint FindPath(int steps, Waypoint startWaypoint, Transform[] potentialTargets, float zeroValue = 0.0f)
    {
        Transform selectedTarget = potentialTargets[0];
        float selectedTargetH = CountHeuristics(startWaypoint, selectedTarget.position);
        for (int i = 1; i < potentialTargets.Length; i++)
        {
            float newH = CountHeuristics(startWaypoint, potentialTargets[i].position);
            if (newH < selectedTargetH)
            {
                selectedTarget = potentialTargets[i];
                selectedTargetH = newH;
            }
        }

        HashSet<Waypoint> visitedWaypoints = new HashSet<Waypoint>();
        LamePriorityQueue<Waypoint> priorityQueue = new LamePriorityQueue<Waypoint>();
        priorityQueue.Enqueue(startWaypoint, CountHeuristics(startWaypoint, selectedTarget.position));

        Waypoint current = null;
        float currentPrior = zeroValue + 1;
        while (steps >= 0 && currentPrior > zeroValue)
        {
            do
            {
                priorityQueue.Dequeue(out current, out currentPrior);
            } while (visitedWaypoints.Contains(current));
            
            visitedWaypoints.Add(current);
            foreach (Waypoint neighbour in current.Neighbours)
            {
                if (neighbour != null && !visitedWaypoints.Contains(neighbour))
                {
                    priorityQueue.Enqueue(neighbour, CountHeuristics(neighbour, selectedTarget.position));
                }
            }

            steps--;
        }

        return current;
    }
}
