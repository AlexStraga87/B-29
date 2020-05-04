using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTurret : EnemyDestroyable
{
    private EnemyDestroyable boss;

    private void Awake()
    {
        boss = transform.parent.GetComponentInParent<EnemyDestroyable>();
    }

    private void OnEnable()
    {
        boss.Dead += BossDead;
    }

    private void OnDisable()
    {
        boss.Dead -= BossDead;
    }

    private void BossDead(GameObject gameObject)
    {
        ResetMoneyReward();
        TakeDamage(100000);
    }
}
