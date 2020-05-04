using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTurret : Shooter
{
    [SerializeField] private BulletBomb _bulletTemplate;
    [SerializeField] private AudioSource _audioSource;

    protected override void Fire()
    {
        StartCoroutine(Shot());
    }

    private IEnumerator Shot()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        for (int i = 0; i < 3; i++)
        {
            BulletBomb bullet = Instantiate(_bulletTemplate, transform.position + transform.up * 0.85f, Quaternion.identity);
            bullet.Initilize(transform.up, _damage, 10);
            _audioSource.Play();
            yield return wait;
        }
        yield return null;
    }

    private void Update()
    {
        Reloading();
        LookToTarget();
        FireOnReadiness();
    }


    private void LookToTarget()
    {
        if (_target == null) return;
        Vector2 direction = _target.transform.position - transform.position;
        var angle = Vector2.SignedAngle(Vector2.up, direction);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
