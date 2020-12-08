using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class CompositeBehaviour : FlockBehaviour
{
    [System.Serializable]
    public struct BehaviourGroup
    {
        public FlockBehaviour behaviour;
        public float weight;
    }

    public BehaviourGroup[] behaviours;

    public FlockBehaviour currentBehaviour;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Life flock)
    {
        Vector2 move = Vector2.zero;
        Vector2 determinantPartialMove = Vector2.zero;

        for (int i = 0; i < behaviours.Length; i++)
        {
            Vector2 partialMove = behaviours[i].behaviour.CalculateMove(agent, context, flock) * behaviours[i].weight;

            if (partialMove != Vector2.zero)
            {
                if (partialMove.sqrMagnitude > behaviours[i].weight * behaviours[i].weight)
                {
                    partialMove.Normalize();
                    partialMove *= behaviours[i].weight;
                    // state machine element -
                    // partial move which makes the most difference to the direction in which the flock goes
                    // defines the state of the flock at the current moment
                    if (partialMove.sqrMagnitude > determinantPartialMove.sqrMagnitude)
                    {
                        determinantPartialMove = partialMove;
                        currentBehaviour = behaviours[i].behaviour;
                    }
                }

                move += partialMove;
            }
        }

        return move;
    }
    
}
