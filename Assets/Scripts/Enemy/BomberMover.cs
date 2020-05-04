using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberMover : EnemyMover
{
    private void Update()
    {
        if (IsMinDistanceToTarget() == false)
        {
            MoveAndRotate();
        }
        else
        {
            RotateToTarget();
        }
    }

}
