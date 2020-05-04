using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyDestroyable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private BossMover _bossMover;
    [SerializeField] private float _deadTime = 3;

    protected override void OnDead()
    {
        AddMoneyToPlayer();

        Component[] destroyables = gameObject.GetComponentsInChildren(typeof(EnemyDestroyable));
        foreach (EnemyDestroyable destroyable in destroyables)
        {
            destroyable.ResetMoneyReward();
            destroyable.TakeDamage(10000);
        }

        if (_effectByDead)
            Instantiate(_effectByDead, transform.position, transform.rotation);
        
        if (_animator)
            _animator.SetBool("IsDead", true);
        _bossMover.enabled = false;
        Destroy(gameObject, _deadTime);
    }


}
