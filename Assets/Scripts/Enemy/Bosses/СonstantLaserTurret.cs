using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class СonstantLaserTurret : Shooter
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private AudioSource _audioSource;
    private bool _isFireing;
    private Coroutine _currentCoroutine;
    private Destroyable _tagetDestroyable;

    protected override void Fire()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }
        _tagetDestroyable = _target.GetComponent<Destroyable>();
        _currentCoroutine = StartCoroutine(LaserShot());
    }

    private void Start()
    {
        _lineRenderer.material.SetColor("_Color", new Color(1, 1, 1, 1 * 0.25f));
    }

    private void Update()
    {        
        if (FindTarget())
        {
            if (_isFireing == false)
            {
                Fire();
                _isFireing = true;
            }
        }
        else if (_isFireing)
        {
            _isFireing = false;
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
            _currentCoroutine = StartCoroutine(LaserShotOff());
        }
    }

    private IEnumerator LaserShot()
    {
        _audioSource.Play();
        _lineRenderer.enabled = true;
        for (int i = 0; i < 4; i++)
        {
            _lineRenderer.material.SetColor("_Color", new Color(1, 1, 1, i * 0.25f));
            _lineRenderer.SetPosition(0, transform.position + transform.up * 0.85f);
            if (_target) _lineRenderer.SetPosition(1, _target.position);
            yield return null;
        }

        while (true)
        {
            LookToTarget();
            Reloading();
            if (_lastFireTime <= 0)
            {
                _tagetDestroyable.TakeDamage(_damage);
                ResetReloadingTime();
            }
            _lineRenderer.SetPosition(0, transform.position + transform.up * 0.85f);
            if (_target) _lineRenderer.SetPosition(1, _target.position);
            yield return null;
        }

    }

    private IEnumerator LaserShotOff()
    {
        int maxCurrentAlpha = (int)(_lineRenderer.material.GetColor("_Color").a * 10);
        
        for (int i = maxCurrentAlpha; i > -1; i--)
        {
            _lineRenderer.material.SetColor("_Color", new Color(1, 1, 1, i * 0.1f));
            _lineRenderer.SetPosition(0, transform.position + transform.up * 0.85f);
            yield return null;
        }
        _audioSource.Stop();
        _lineRenderer.enabled = false;
        _currentCoroutine = null;
    }

    private void LookToTarget()
    {
        Vector2 direction = _target.transform.position - transform.position;
        var angle = Vector2.SignedAngle(Vector2.up, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), 10 * Time.deltaTime);
    }
}
