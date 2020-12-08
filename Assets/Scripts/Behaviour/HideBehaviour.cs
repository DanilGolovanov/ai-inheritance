using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Hide")]
public class HideBehaviour : FilteredFlockBehaviour
{
    public ContextFilter obstaclesFilter;

    public float hideBehindObstaclesDistance = 2f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Life flock)
    {
        if (context.Count == 0)
        {
            return Vector2.zero;
        }
        // Hide from
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        // Hide behind
        List<Transform> obstaclesContext = (filter == null) ? context : obstaclesFilter.Filter(agent, context);

        if (filteredContext.Count == 0)
        {
            return Vector2.zero;
        }

        // Find neareset obstacle to hide behind
        float nearestDistance = float.MaxValue;
        Transform nearestObstacle = null;
        foreach (Transform item in obstaclesContext)
        {
            float distance = Vector2.Distance(item.position, agent.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestObstacle = item;
            }
        }

        // If no obstacle 
        if (nearestObstacle == null)
        {
            return Vector2.zero;
        }

        // Find best hiding spot
        Vector2 hidePosition = Vector2.zero;
        foreach (Transform item in filteredContext)
        {
            Vector2 obstacleDirection = nearestObstacle.position - item.position;

            obstacleDirection = obstacleDirection.normalized * hideBehindObstaclesDistance;

            hidePosition += (Vector2)item.position + obstacleDirection;
        }
        hidePosition /= filteredContext.Count;

        // FOR DEBUG ONLY
        Debug.DrawRay(hidePosition, Vector2.up * 1f);

        return hidePosition - (Vector2)agent.transform.position;
    }

    public override string ToString()
    {
        return "Hide";
    }
}
