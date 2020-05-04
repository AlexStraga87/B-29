using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    [SerializeField] protected int _damage = 1;
    [SerializeField] protected float _range = 10;
    [SerializeField] protected float _fireReloadTime = 2;
    [SerializeField] protected bool _isAsteroidTarget = true;

    protected float _lastFireTime = 2;    
    protected Player _player;
    protected Station _station;
    protected Transform _target;

    public void SetTargets(Player player, Station station)
    {
        _player  = player;
        _station = station;
    }

    protected void Reloading()
    {
        _lastFireTime -= Time.deltaTime;
    }

    protected void ResetReloadingTime()
    {
        _lastFireTime = _fireReloadTime;
    }

    protected abstract void Fire();

    protected void FireOnReadiness()
    {
        if (_lastFireTime < 0)
        {
            if (FindTarget())
            {
                Fire();
                ResetReloadingTime();
            }
        }
    }

    protected bool FindTarget()
    {
        _target = null;
        float distancePlayer = Vector2.Distance((Vector2)transform.position, (Vector2)_player.transform.position);
        float distanceStation = Vector2.Distance((Vector2)transform.position, (Vector2)_station.transform.position);

        if (distanceStation < _range && distancePlayer > distanceStation)
        {
            _target = _station.transform;
            return true;
        }

        if (distancePlayer < _range && distancePlayer < distanceStation)
        {
            _target = _player.transform;
            return true;
        }

        if (_isAsteroidTarget)
        {
            Collider2D[] contextColliders = Physics2D.OverlapCircleAll(transform.position, _range);
            foreach (Collider2D collider in contextColliders)
            {
                Asteroid asteroid = collider.GetComponent<Asteroid>();

                if (asteroid != null)
                {
                    _target = asteroid.transform;
                    return true;
                }
            }
        }
        return false;
    }
}
