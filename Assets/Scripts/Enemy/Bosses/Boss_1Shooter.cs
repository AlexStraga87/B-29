using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1Shooter : Shooter
{
    [SerializeField] private Flock _flockTemplate;
    [SerializeField] private int _flockCount = 4;

    private void Update()
    {
        Reloading();
        FireOnReadiness();
    }

    private void CreateFlock()
    {
        Flock flock = Instantiate(_flockTemplate, transform.position + (Vector3)(Random.insideUnitCircle * 1 + GetRandomPosOnCircle() * 6.5f), Quaternion.identity);
        flock.SetTarget(_player, _station);
        flock.SetAgentCount(2);
    }

    private IEnumerator CreateFlockCoroutine()
    {
        for (int i = 0; i < _flockCount; i++)
        {
            CreateFlock();
            yield return new WaitForSeconds(0.2f);
        }

    }

    private Vector2 GetRandomPosOnCircle()
    {
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        Vector2 position = new Vector2();
        position.x = Mathf.Cos(angle);
        position.y = Mathf.Sin(angle);

        return position;
    }

    protected override void Fire()
    {
        StartCoroutine(CreateFlockCoroutine());
    }
}
