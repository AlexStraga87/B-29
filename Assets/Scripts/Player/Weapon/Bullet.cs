using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public  class Bullet : MonoBehaviour
{
    [SerializeField] protected GameObject _effectOnDestroy;
    [SerializeField] protected int _speed = 20;
    protected int _damage;
    protected float _lifeTime = 5;
    protected Vector2 _direction;
    protected Rigidbody2D _rigidbody2D;

    public void Initilize(Vector2 direction, int damage)
    {
        _damage = damage;
        _direction = direction;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = direction.normalized * _speed;
    }

    protected void Aging()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected void StandartCollision(Collider2D collision)
    {
        Destroyable destroyable = collision.GetComponent<Destroyable>();
        if (destroyable != null)
        {
            if (_effectOnDestroy)
                Instantiate(_effectOnDestroy, transform.position, Quaternion.identity);
            destroyable.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

}
