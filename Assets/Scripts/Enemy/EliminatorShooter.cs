using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliminatorShooter : Shooter
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private AudioSource _audioSource;
    private Vector3 _laserOffset = new Vector3();

    private void Update()
    {
        Reloading();
        FireOnReadiness();
    }

    protected override void Fire()
    {
        _laserOffset = (Vector3)Random.insideUnitCircle * 0.5f;
        StartCoroutine(LaserShot());
        _target.GetComponent<Destroyable>().TakeDamage(_damage);
    }

    private IEnumerator LaserShot()
    {
        _audioSource.Play();
        _lineRenderer.enabled = true;
        for (int i = 0; i < 4; i++)
        {
            _lineRenderer.material.SetColor("_Color", new Color(1, 1, 1, i * 0.25f));
            _lineRenderer.SetPosition(0, transform.position);
            if (_target) _lineRenderer.SetPosition(1, _target.position + _laserOffset);
            yield return null;
        }

        for (int i = 10; i > -1; i--)
        {
            _lineRenderer.material.SetColor("_Color", new Color(1, 1, 1, i * 0.1f));
            _lineRenderer.SetPosition(0, transform.position);
            if (_target) _lineRenderer.SetPosition(1, _target.position + _laserOffset);
            yield return null;
        }
        _lineRenderer.enabled = false;
    }

}
