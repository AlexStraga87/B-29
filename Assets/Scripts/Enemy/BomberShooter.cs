using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberShooter : Shooter
{
    [SerializeField] BulletBomb _bulletTemplate;

    private void Update()
    {
        Reloading();
        FireOnReadiness();
    }

    protected override void Fire()
    {
        Vector2 direction = (_target.position - transform.position).normalized;
        BulletBomb bullet = Instantiate(_bulletTemplate, (Vector2)transform.position + direction * 0.6f, Quaternion.identity);
        bullet.Initilize(direction, _damage);
    }
}
