using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suicider : EnemyDestroyable
{
    protected override void OnTakeDamage(int damage)
    {
        _hp -= damage;
    }

}
