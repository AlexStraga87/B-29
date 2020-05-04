using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SuperLaserGun : MonoBehaviour
{
    public event UnityAction StopShooting;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private BossMoverWayPoint _bossMover;
    private int _cycles = 100;
    private int _damage = 1;
    private Destroyable _target;
    private Vector2 _endPoint;

    private void OnEnable()
    {
        _bossMover.WayPoint += Fire;
    }

    private void OnDisable()
    {
        _bossMover.WayPoint -= Fire;
    }

    private void Fire()
    {
        StartCoroutine(LaserShot());
    }

    private IEnumerator LaserShot()
    {
        _audioSource.Play();

        for (int i = 0; i < 80; i++)
        {
            SetLaserSetting(i * 0.013f, i * 0.0055f);
            yield return null;
        }

        for (int i = 0; i < _cycles; i++)
        {
            SetLaserSetting(1, 1.7f + Random.Range(0, 0.3f));
            if (_target)
                _target.TakeDamage(_damage);

            yield return null;
        }

        WaitForSeconds wait = new WaitForSeconds(0.15f);
        for (int i = 0; i < 3; i++)
        {
            SetLaserSetting(0.5f, 0.55f);
            yield return wait;
            SetLaserSetting(0.0f, 0.55f);
            yield return wait;
        }
        StopShooting?.Invoke();

    }

    private void SetLaserSetting(float alpha, float widthLine)
    {
        _lineRenderer.material.SetColor("_Color", new Color(1, 1, 1, alpha));
        _lineRenderer.widthMultiplier = widthLine;
        TrackLaserEnd();
        _lineRenderer.SetPosition(0, _startPoint.position);
        _lineRenderer.SetPosition(1, _endPoint);
    }

    private void TrackLaserEnd()
    {
        _target = null;
        _endPoint = _startPoint.position + Vector3.left * 30;
        RaycastHit2D[] hits = Physics2D.RaycastAll(_startPoint.position, Vector3.left, 30, _layer);
        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.TryGetComponent(out Destroyable destroyable))
            {
                _target = destroyable;
                _endPoint = hit.point;
                return;
            }
        }
    }
}
