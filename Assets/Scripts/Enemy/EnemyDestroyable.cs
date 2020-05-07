using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDestroyable : Destroyable
{
    [SerializeField] protected GameObject _effectByDead;
    [SerializeField] protected int _money = 0;
    private SaveSystem _saveSystem;

    public void SetSaveSystem(SaveSystem saveSystem)
    {
        _saveSystem = saveSystem;
    }

    public SaveSystem GetSaveSystem()
    {
        return _saveSystem;
    }

    public void ResetMoneyReward()
    {
        _money = 0;
    }

    protected override void OnDead()
    {
        if (_effectByDead)
             Instantiate(_effectByDead, transform.position, transform.rotation);

        AddMoneyToPlayer();
        ResetMoneyReward();
        Destroy(gameObject);
    }

    public void AddMoneyToPlayer()
    {
        if (_money > 0)
            _saveSystem.AddMoney(_money);
    }

    protected override void OnTakeDamage(int damage)
    {
        _hp -= damage;
    }
}
