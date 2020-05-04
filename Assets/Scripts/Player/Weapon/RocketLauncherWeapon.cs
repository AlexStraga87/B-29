using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherWeapon : Weapon
{
    private bool _isSmart = false;

    private void Update()
    {
        Reloading();
    }

    protected override void ConfigurateBullet(Bullet bullet)
    {
        if (_isSmart)
            (bullet as RocketBullet).SetSmartRocket();
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
                _isSmart = true;
                break;
        }

    }


}
