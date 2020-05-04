using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuiciderShooter : Shooter
{
    [SerializeField] protected int _explotionForce = 100;

    private void Update()
    {
        if (IsStationInRange())
            Fire();
    }

    private bool IsStationInRange()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.up, _range);
        foreach (var hit in hits)
        {
            if (hit.transform.GetComponentInParent<Station>())
            {
                return true;
            }
        }
        return false;
    }

    protected override void Fire()
    {
        Explode();
        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _range * 2);
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody2D rigidBody2D))
            {
                Vector2 direction = rigidBody2D.transform.position - transform.position;
                float magnitude = direction.magnitude;
                if (magnitude < 1) magnitude = 1;
                //direction = direction.normalized * (_range * 2 / magnitude);
                direction = direction.normalized * (1 - magnitude / (_range * 2));
                rigidBody2D.AddForce(direction * _explotionForce, ForceMode2D.Impulse);
            }
            if (collider.TryGetComponent(out Destroyable destroyable))
            {
                destroyable.TakeDamage(_damage);
            }

        }
    }

}
