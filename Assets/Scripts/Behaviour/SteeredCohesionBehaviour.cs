using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Steered Cohesion")]
public class SteeredCohesionBehaviour : FilteredFlockBehaviour
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Life flock)
    {
        // if no neighbours, return no adjustment   
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        // add all points together and average
        Vector2 cohesionMove = Vector2.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        int count = 0;
        foreach (Transform item in filteredContext)
        {
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareSmallRadius)
            {
                cohesionMove += (Vector2)item.position;
                count++;
            }
        }
        if (count != 0)
        {
            cohesionMove /= count;
        }

        // create offset from agent position
        cohesionMove -= (Vector2)agent.transform.position;
        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime);
        return cohesionMove;
    }

    public override string ToString()
    {
        return "SteeredCohesion";
    }
}
