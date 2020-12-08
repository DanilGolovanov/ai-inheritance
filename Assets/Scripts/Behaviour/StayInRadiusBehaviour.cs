using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Stay In Radius")]
public class StayInRadiusBehaviour : FlockBehaviour
{
    [SerializeField]
    private Vector2 center;
    [SerializeField]
    private float radius = 15;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Life flock)
    {
        // direction to the center
        // magnitude will be the distance to the center
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float t = centerOffset.magnitude / radius;

        if (t < 0.9f)
        {
            return Vector2.zero;
        }

        return centerOffset * t * t;
    }

    public override string ToString()
    {
        return "StayInRadius";
    }
}
