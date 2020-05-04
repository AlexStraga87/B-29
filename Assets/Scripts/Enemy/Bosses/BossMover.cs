using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossMover : EnemyMover
{
    private void Start()
    {
        _minDistanceToTarget = 25;
        SetTarget(GameObject.Find("Station").transform);
    }

    private void Update()
    {
        if (IsMinDistanceToTarget() == false)
        {
            MoveToTarget();
        }
        else
        {
            MoveOrbitTarget(90);
        }
    }

    public void DecreaseSpeed(int speed)
    {
        _speed -= speed;
    }
}
