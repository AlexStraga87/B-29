using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    public Flock AgentFlock => _agentFlock;
    public Collider2D AgentCollider => _agentCollider;
    public Vector2 Velocity => _velocity;
    public event UnityAction<FlockAgent> Dead;

    private Flock _agentFlock;
    private Collider2D _agentCollider;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _velocity = new Vector2();

    


    private void Start()
    {
        _agentCollider = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnDestroy()
    {
        Dead?.Invoke(this);
    }

    public void Initialize(Flock flock)
    {
        _agentFlock = flock;
    }

    public void SetVelocity(Vector2 velocity)
    {
        _velocity = Vector2.Lerp(_velocity, velocity, 10 * Time.deltaTime);
    }

    public void Move()
    {
        transform.position += (Vector3)_velocity * Time.deltaTime;
    }

    private void Update()
    {
        transform.up = Vector2.Lerp(transform.up, _velocity, 5 * Time.deltaTime);
    }


}
