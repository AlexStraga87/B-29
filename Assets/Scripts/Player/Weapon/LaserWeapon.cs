using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : Weapon
{
    protected override void ConfigurateBullet(Bullet bullet)
    {
        (bullet as LaserBullet).Shoot();
    }

    public override void Upgrade(int level)
    {
        switch (level)
        {
            case 0:
                _isUnlocked = false;
                this.enabled = false;
                break;
            case 2:
                _shootCost = (int)(_shootCost * 0.70f);
                break;
        }
    }

    private void Update()
    {
        Reloading();
    }


}
