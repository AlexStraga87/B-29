using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Destroyable : MonoBehaviour
{
    public event UnityAction<GameObject> Dead;
    [SerializeField] protected float _hp = 30;
    [SerializeField] protected float _hpMax = 30;
    [SerializeField] protected bool _isImmortal = false;

    protected abstract void OnDead();
    protected abstract void OnTakeDamage(int damage);

    public void SetImmortal()
    {
        _isImmortal = true;
    }

    public void TakeDamage(int damage)
    {
        if (_isImmortal)
            return;

        OnTakeDamage(damage);
        if (_hp <= 0)
        {
            SetImmortal();
            Dead?.Invoke(gameObject);
            OnDead();
        }
    }



}
