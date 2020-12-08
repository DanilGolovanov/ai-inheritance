﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : FilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Life flock)
    {
        if (context.Count == 0)
        {
            return Vector2.zero;
        }

        //getting average
        Vector2 avoidanceMove = Vector2.zero;
        int numAvoid = 0;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                numAvoid++;
                avoidanceMove += (Vector2)(agent.transform.position - item.position);
            }
        }

        if (numAvoid > 0)
        {
            avoidanceMove /= numAvoid;
        }

        return avoidanceMove;
    }

    public override string ToString()
    {
        return "Avoidance";
    }
}
