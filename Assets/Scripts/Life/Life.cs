using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlockType
{
    Predator,
    Prey
}

public class Life : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public CompositeBehaviour behaviour;

    [Range(10, 500)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighbourRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;
    [Range(0f, 1f)]
    public float smallRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighbourRadius;
    float squareAvoidanceRadius;
    float squareSmallRadius;

    public float SquareAvoidanceRadius { get => squareAvoidanceRadius; }
    public float SquareSmallRadius { get => squareSmallRadius; }

    protected FlockType flockType;

    public void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighbourRadius = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        squareSmallRadius = squareNeighbourRadius * smallRadiusMultiplier * smallRadiusMultiplier;

        CreateLife(flockType);
    }

    protected void CreateLife(FlockType flockType)
    {
        //loops for startingCount times
        for (int i = 0; i < startingCount; i++)
        {
            //create a new agent (the agent is the AI)
            FlockAgent newAgent = Instantiate( //instantiate creates a clone of a gameobject or prefab
                agentPrefab, //this is the prefab being cloned
                Random.insideUnitCircle * startingCount * AgentDensity, // give in a random position within a circle
                Quaternion.Euler(Vector3.forward * Random.Range(0, 360f)), //give it a random rotation
                transform  //this transform is the parent of the new AI
                );
            newAgent.name = flockType.ToString() + " " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }

    public void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            //change color of agents
            //agent.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);
            Vector2 move = behaviour.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.GetComponent<Animator>().Play(behaviour.currentBehaviour.ToString());
            agent.Move(move);
        }
    }

    private List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighbourRadius);

        foreach (Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }

        return context;
    }
}
