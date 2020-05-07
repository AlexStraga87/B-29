using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Flock : MonoBehaviour
{
    [SerializeField] private int _startingCount = 250;
    [SerializeField] private FlockAgent _agentPrefab;
    [SerializeField] private float _driveFactor = 10f;
    [SerializeField] private float _maxSpeed = 7f;
    [SerializeField] private float _neighborRadius = 6f;
    [SerializeField] private float _avoidanceRadiusMultiplier = 0.33f;
    [SerializeField] private float _cohesionWeight = 2f;
    [SerializeField] private float _alingmentWeight = 2f;
    [SerializeField] private float _avoidanceWeight = 2f;
    [SerializeField] private float _obstacleWeight = 3f;
    [SerializeField] private float _targetWeight = 1f;

    private Player _player;
    private Station _station;
    private List<FlockAgent> _agents = new List<FlockAgent>();
    private SaveSystem _saveSystem;

    private float _squareMaxSpeed;
    private float _squareNeighborRadius;
    private float _squareAvoidanceRadius;
    private Vector2 _currentVelocity;
    private float _agentSmoothTime = 0.5f;
    private const float _AgentDensity = 0.08f;

    private List<FlockAgent> _nearbyAgents;
    private List<Transform> _nearbyObstacles;

    public event UnityAction<GameObject> Dead;

    private void OnValidate()
    {
        _squareMaxSpeed = _maxSpeed * _maxSpeed;
        _squareNeighborRadius = _neighborRadius * _neighborRadius;
        _squareAvoidanceRadius = _squareNeighborRadius * _avoidanceRadiusMultiplier * _avoidanceRadiusMultiplier;
    }

    private void Start()
    {
        _squareMaxSpeed = _maxSpeed * _maxSpeed;
        _squareNeighborRadius = _neighborRadius * _neighborRadius;
        _squareAvoidanceRadius = _squareNeighborRadius * _avoidanceRadiusMultiplier * _avoidanceRadiusMultiplier;

        for (int i = 0; i < _startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                _agentPrefab,
                (Vector2)transform.position + Random.insideUnitCircle * _startingCount * _AgentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );
            newAgent.name = "Eliminator " + i;
            newAgent.Dead += RemoveAgent;
            newAgent.Initialize(this);
            newAgent.GetComponent<EnemyDestroyable>().SetSaveSystem(_saveSystem);
            _agents.Add(newAgent);
            newAgent.GetComponent<Shooter>().SetTargets(_player, _station);
        }
    }
    
    public void SetSaveSystem(SaveSystem saveSystem)
    {
        _saveSystem = saveSystem;
    }

    public void SetAgentCount(int count)
    {
        _startingCount = count;
    }

    public void SetTarget(Player player, Station station)
    {
        _player = player;
        _station = station;
    }

    private void Update()
    {
        foreach (FlockAgent agent in _agents)
        {
            GetNearbyObjects(agent);

            Vector2 move = Vector2.zero;

            move += SteeredCohesionMove(agent, _nearbyAgents, _cohesionWeight);            
            move += AlingmentMove(agent, _nearbyAgents, _alingmentWeight);
            move += AvoidanceMove(agent, _nearbyAgents, _avoidanceWeight);
            move += ObstacleMove(agent, _nearbyObstacles, _obstacleWeight);
            move += TargetMove(agent, _targetWeight);

            move *= _driveFactor;
            if (move.sqrMagnitude > _squareMaxSpeed)
            {
                move = move.normalized * _maxSpeed;
            }
            agent.SetVelocity(move);
            
        }
        foreach (FlockAgent agent in _agents)
        {
            agent.Move();
        }
        if (_agents.Count == 0)
        {
            Dead?.Invoke(gameObject);
            Destroy(gameObject);
        }
    }

    private void GetNearbyObjects(FlockAgent agent)
    {
        _nearbyAgents = new List<FlockAgent>();
        _nearbyObstacles = new List<Transform>();

        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, _neighborRadius);
        foreach (Collider2D collider in contextColliders)
        {

            FlockAgent checkingAgent = collider.GetComponent<FlockAgent>();

            if (checkingAgent != null)
            {
                if (collider != agent.AgentCollider)
                {
                    _nearbyAgents.Add(checkingAgent);
                }
            }
            else 
            {
                _nearbyObstacles.Add(collider.transform);
            }
        }
        
    }

    private bool CheckConeVisible(Transform positionOriginal, Vector2 positionObject, float coneAngle = 120)
    {
        Vector2 direction = positionObject - (Vector2)positionOriginal.position;
        float currentAngle = Vector2.Angle(direction, positionOriginal.up);
        if (currentAngle < coneAngle / 2)
            return true;
        return false; 
    }

    private Vector2 SteeredCohesionMove(FlockAgent agent, List<FlockAgent> context, float weight)
    {        
        if (context.Count == 0)
            return Vector2.zero;

        Vector2 cohesionMove = Vector2.zero;
        foreach (FlockAgent _agent in context)
        {
            cohesionMove += (Vector2)_agent.transform.position;
        }
        cohesionMove /= context.Count;

        cohesionMove -= (Vector2)agent.transform.position;
        cohesionMove = Vector2.SmoothDamp(agent.Velocity, cohesionMove, ref _currentVelocity, _agentSmoothTime);
        cohesionMove = CalculateMoveWithWeight(cohesionMove, weight);
        return cohesionMove;
    }

    private Vector2 AlingmentMove(FlockAgent agent, List<FlockAgent> context, float weight)
    {
        if (context.Count == 0)
            return agent.transform.up;

        Vector2 alignmentMove = Vector2.zero;
        foreach (FlockAgent _agent in context)
        {
            alignmentMove += (Vector2)_agent.Velocity;
        }
        alignmentMove /= context.Count;

        alignmentMove = CalculateMoveWithWeight(alignmentMove, weight);
        return alignmentMove;
    }

    private Vector2 AvoidanceMove(FlockAgent agent, List<FlockAgent> context, float weight)
    {
        if (context.Count == 0)
            return Vector2.zero;

        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;
        foreach (FlockAgent _agent in context)
        {
            if (Vector2.SqrMagnitude(_agent.transform.position - agent.transform.position) < _squareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += (Vector2)(agent.transform.position - _agent.transform.position);
            }
        }
        if (nAvoid > 0)
            avoidanceMove /= nAvoid;

        avoidanceMove = CalculateMoveWithWeight(avoidanceMove, weight);
        return avoidanceMove;
    }

    private Vector2 ObstacleMove(FlockAgent agent, List<Transform> context, float weight)
    {
        if (context.Count == 0)
            return Vector2.zero;

        Vector2 obstacleMove = Vector2.zero;
        int nObstacles = 0;
        foreach (Transform item in context)
        {
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) < _squareNeighborRadius)
            {
                nObstacles++;
                obstacleMove += (Vector2)(agent.transform.position - item.position);
            }
        }
        if (nObstacles > 0)
            obstacleMove /= nObstacles;

        obstacleMove = CalculateMoveWithWeight(obstacleMove, weight);
        return obstacleMove;
    }

    private Vector2 TargetMove(FlockAgent agent, float weight)
    {
        Vector2 centerOffset = Vector2.zero - (Vector2)agent.transform.position;
        float t = centerOffset.magnitude / 15;
        if (t < 0.9f)
        {
            return Vector2.zero;
        }
        return CalculateMoveWithWeight(centerOffset * t * t, weight);        
    }

    private void RemoveAgent(FlockAgent agent)
    {
        agent.Dead -= RemoveAgent;
        _agents.Remove(agent);
    }

    private Vector2 CalculateMoveWithWeight(Vector2 speed, float weight)
    {        
        if (speed != Vector2.zero)
        {
            if (speed.sqrMagnitude > weight * weight)
            {
                speed.Normalize();
                speed *= weight;
            }
            return speed;
        }
        return Vector2.zero;
    }

}
