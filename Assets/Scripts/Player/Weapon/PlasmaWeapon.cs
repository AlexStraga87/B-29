using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaWeapon : Weapon
{
    private void Update()
    {
        Reloading();
    }

    public override void Upgrade(int level)
    {
        if (level == 2)
        {
            _damage++;
        }
    }

}
