using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class BulletBomb : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _explotionTemplate;
    private float _lifeTime = 6;
    private int _damage;
    private Vector2 _direction;
    private Rigidbody2D _rigidbody2D;

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Initilize(Vector2 direction, int damage, float speed = 0)
    {
        _direction = direction;
        _damage = damage;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (speed != 0) _speed = speed;
        _rigidbody2D.velocity = direction.normalized * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroyable destroyable = collision.GetComponent<Destroyable>();
        if (destroyable != null)
        {
            destroyable.TakeDamage(_damage);
            Explotion();
            Destroy(gameObject);
        }
    }

    private void Explotion()
    {
        var explotion = Instantiate(_explotionTemplate, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        explotion.transform.localScale = Vector3.one * 0.2f;
    }
}
