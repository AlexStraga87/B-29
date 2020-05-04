using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvesterMover : EnemyMover
{
    private void Update()
    {
        MoveAndRotate();
        if (IsMinDistanceToTarget())
        {
            var harvester = GetComponent<EnemyDestroyable>();
            harvester.ResetMoneyReward();
            harvester.TakeDamage(10000);
        }
    }

}
