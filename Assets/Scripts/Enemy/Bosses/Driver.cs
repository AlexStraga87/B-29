using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : EnemyDestroyable
{
    [SerializeField] private BossMover _bossMover;
    [SerializeField] private int _speed = 5;

    protected override void OnDead()
    {
        _bossMover.DecreaseSpeed(_speed);
        base.OnDead();
    }
}
