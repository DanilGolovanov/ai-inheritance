using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    Life agentFlock;
    public Life AgentFlock
    {
        get => agentFlock;
    }
    private Collider2D agentCollider;
    public Collider2D AgentCollider
    {
        get => agentCollider;
    }

    private void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Initialize(Life flock)
    {
        agentFlock = flock;
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
