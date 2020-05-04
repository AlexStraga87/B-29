using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(SuiciderShooter))]
public class SuiciderMover : EnemyMover
{    
    private void Update()
    {
        MoveAndRotate();
    }

}
