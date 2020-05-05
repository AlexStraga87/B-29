using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerDataLoader))]
public class StationLaser : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _damage = 20;
    [SerializeField] private float _maxDistance = 14;
    [SerializeField] private float _reloadTime = 10;
    [SerializeField] private float _lastFireTime = 10;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private PlayerDataLoader _playerDataLoader;

    private EnemyDestroyable _target;

    private void Start()
    {
        Upgrades();
    }

    protected void Upgrades()
    {
        PlayerData playerData = _playerDataLoader.GetPlayerData();
        int level = playerData.Upgrades[(int)UpgradesList.StationGun];
        switch (level)
        {
            case 0:
                this.enabled = false;
                break;
            case 2:
                _reloadTime = _reloadTime / 2;
                break;
        }
    }

    private void Update()
    {
        _lastFireTime -= Time.deltaTime;
        if (_lastFireTime <= 0)
        {
            if (FindTarget())
            {
                Fire();
            }
        }
    }

    private void Fire()
    {
        _lastFireTime = _reloadTime;
        _lineRenderer.SetPosition(1, _target.transform.position);
        _target.TakeDamage(_damage);
        _audioSource.Play();
        StartCoroutine(LaserShot());
    }

    private bool FindTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _maxDistance);
        List<EnemyDestroyable> targets = new List<EnemyDestroyable>();

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out EnemyDestroyable enemy))
            {
                targets.Add(enemy);
            }
        }
        if (targets.Count > 0)
        {
            _target = targets[Random.Range(0, targets.Count)];
            return true;
        }

        return false;
    }

    private IEnumerator LaserShot()
    {
        _lineRenderer.enabled = true;
        for (int i = 0; i < 8; i++)
        {
            ChangeLaserAlpha(i * 0.125f);
            yield return null;
        }

        for (int i = 20; i > -1; i--)
        {
            ChangeLaserAlpha(i * 0.05f);
            yield return null;
        }
        ChangeLaserAlpha(0);
    }

    private void ChangeLaserAlpha(float alpha)
    {
        _lineRenderer.material.SetColor("_Color", new Color(1, 0.4f, 0.4f, alpha));
    }
}
