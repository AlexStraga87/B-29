using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Destroyable
{
    [SerializeField] protected int _randomHPMin = 100;
    [SerializeField] protected int _randomHPMax = 300;
    [SerializeField] protected GameObject _effect;

    private void Awake()
    {
        _hp = Random.Range(_randomHPMin, _randomHPMax);
    }

    protected override void OnDead()
    {
        Instantiate(_effect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    protected override void OnTakeDamage(int damage)
    {
        _hp -= damage;
    }
}
