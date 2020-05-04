using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBullet : Bullet
{    
    [SerializeField] private LayerMask _layer;
    private bool _isSmart = false;

    private void Start()
    {
        _speed = 15;
    }

    public void SetSmartRocket()
    {
        _isSmart = true;
    }

    private void Update()
    {
        Moving();
        Aging();
    }

    private void Moving()
    {
        if (_isSmart)
            FindEnemies();

       _rigidbody2D.velocity = transform.up * _speed;
    }

    private void FindEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 15, _layer);
        float enemyDistance = 100;
        Transform enemy = null;
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<EnemyDestroyable>())
            {
                float dist = Vector2.Distance(transform.position, collider.transform.position);
                if (dist < enemyDistance)
                {
                    enemy = collider.transform;
                    enemyDistance = dist;
                }
            }
        }
        if (enemy != null)
        {
            Vector2 direction = enemy.transform.position - transform.position;
            var angle = Vector2.SignedAngle(Vector2.up, direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), 5 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Destroyable>() != null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2.5f, _layer);
            foreach (Collider2D collider in colliders)
            {
                if (collider.TryGetComponent(out Destroyable destroyable))
                {
                    destroyable.TakeDamage(_damage);
                }
            }

            if (_effectOnDestroy)
                Instantiate(_effectOnDestroy, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}
