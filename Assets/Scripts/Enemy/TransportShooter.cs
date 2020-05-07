using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportShooter : Shooter
{
    [SerializeField] Suicider _template;
    private SaveSystem _saveSystem;

    private void Start()
    {
        _saveSystem = GetComponent<EnemyDestroyable>().GetSaveSystem();
    }

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
        SetSuiciderSettings(suicider);
        suicider = Instantiate(_template, (Vector2)(transform.position + transform.right * 2.5f), Quaternion.identity);
        SetSuiciderSettings(suicider);
    }

    private void SetSuiciderSettings(Suicider suicider)
    {
        suicider.GetComponent<Shooter>().SetTargets(_player, _station);
        suicider.GetComponent<EnemyMover>().SetTarget(_station.transform);
        suicider.GetComponent<Suicider>().SetSaveSystem(_saveSystem);
    }

}
