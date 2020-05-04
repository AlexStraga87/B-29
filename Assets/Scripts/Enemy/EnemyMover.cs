using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] protected int _speed = 5;
    [SerializeField] protected int _rayDistance = 5;
    [SerializeField] protected int _minDistanceToTarget = 5;

    protected Transform _target;
    protected Vector2 _velocity;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    protected void MoveAndRotate()
    {
        Vector2 direction = direction = _target.transform.position - transform.position;

        Vector2 leftRay = transform.position - transform.right * 0.5f; //0.5f
        Vector2 rightRay = transform.position + transform.right * 0.5f;

        direction += GetDirectionOffset(transform.position);
        direction += GetDirectionOffset(leftRay);
        direction += GetDirectionOffset(rightRay);

        var angle = Vector2.SignedAngle(Vector2.up, direction);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), 6 * Time.deltaTime);
        transform.position += transform.up * _speed * Time.deltaTime;
    }

    protected void RotateToTarget()
    {
        Vector2 direction = _target.transform.position - transform.position;
        var angle = Vector2.SignedAngle(Vector2.up, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), 6 * Time.deltaTime);
    }

    protected void MoveForward()
    {        
        transform.position += transform.up * _speed * Time.deltaTime;
    }

    protected void MoveToTarget()
    {
        Vector2 direction = _target.transform.position - transform.position;
        transform.position += (Vector3)direction.normalized * _speed * Time.deltaTime;
    }

    protected void MoveOrbitTarget(float angle)
    {
        Vector2 direction = _target.transform.position - transform.position;
        direction = Quaternion.Euler(0, 0, angle) * direction;
        transform.position += (Vector3)direction.normalized * _speed * Time.deltaTime;
    }

    protected bool IsMinDistanceToTarget()
    {
        Vector2 direction = _target.transform.position - transform.position;

        return direction.magnitude <= _minDistanceToTarget;
    }

    protected bool IsTargetVisible()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, _target.position, _rayDistance);
        bool isVisible = true;
        foreach (var hit in hits)
        {
            if (hit.transform != transform && hit.transform != _target)
            {
                isVisible = false;
            }
        }

        return isVisible;
    }

    private Vector2 GetDirectionOffset(Vector2 startRayPoint)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(startRayPoint, transform.up, _rayDistance);

        foreach (var hit in hits)
        {
            if (hit.transform != transform)
            {
                return (Vector2)hit.normal * 40;
            }
        }
        return Vector2.zero;
    }
}
