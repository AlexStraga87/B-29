using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportShooter : Shooter
{
    [SerializeField] Suicider _template;

    private void Update()
    {
        Reloading();
        FireOnReadiness();
    }

    protected override void Fire()
    {
        Vector2 direction = (_target.position - transform.position).normalized;
        Suicider suicider;

        suicider = Instantiate(_template, (Vector2)(transform.position - transform.right * 2.5f), Quaternion.identity);
        SetSuiciderTarget(suicider);
        suicider = Instantiate(_template, (Vector2)(transform.position + transform.right * 2.5f), Quaternion.identity);
        SetSuiciderTarget(suicider);
    }

    private void SetSuiciderTarget(Suicider suicider)
    {
        suicider.GetComponent<Shooter>().SetTargets(_player, _station);
        suicider.GetComponent<EnemyMover>().SetTarget(_station.transform);
    }

}
