using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Wander")]
public class WanderBehaviour : FilteredFlockBehaviour
{
    Path path = null;
    private int currentWaypoint = 0;

    Vector2 waypointDirection = Vector2.zero;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Life flock)
    {
        if (path == null)
        {
            FindPath(agent, context);
        }

        return FollowPath(agent);
    }

    private Vector2 FollowPath(FlockAgent agent)
    {
        if (path == null)
        {
            return Vector2.zero;
        }

        if (InRadius(agent))
        {
            currentWaypoint++;
            if (currentWaypoint >= path.waypoints.Count)
            {
                currentWaypoint = 0;
            }

            return Vector2.zero;
        }

        return waypointDirection;
    }

    private bool InRadius(FlockAgent agent)
    {
        //direction to the waypoint
        waypointDirection = (Vector2)path.waypoints[currentWaypoint].position - (Vector2)agent.transform.position;

        //waypointDirection.magnitude would give us direction to the waypoint
        if (waypointDirection.magnitude < path.radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void FindPath(FlockAgent agent, List<Transform> context)
    {
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        if (filteredContext.Count == 0)
        {
            return;
        }

        int randomPathIndex = UnityEngine.Random.Range(0, filteredContext.Count);
        path = filteredContext[randomPathIndex].GetComponentInParent<Path>();
    }

    public override string ToString()
    {
        return "Wander";
    }
}
