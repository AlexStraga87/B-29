using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserBullet : Bullet
{
    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] LayerMask _layer;
    [SerializeField] Transform _effect;
    public void Shoot()
    {
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, transform.position + transform.up * 30);

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.up, 30, _layer);
        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.TryGetComponent(out Destroyable destroyable))
            {
                destroyable.TakeDamage(_damage);
                Instantiate(_effect, hit.point, Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, transform.up)));
            }
        }
        StartCoroutine(LaserShot());

    }

    private IEnumerator LaserShot()
    {
        _lineRenderer.enabled = true;
        for (int i = 0; i < 4; i++)
        {
            _lineRenderer.material.SetColor("_Color", new Color(1, 0.4f, 0.4f, i * 0.25f));
            yield return null;
        }

        for (int i = 10; i > -1; i--)
        {
            _lineRenderer.material.SetColor("_Color", new Color(1, 0.4f, 0.4f, i * 0.1f));
            yield return null;
        }

        Destroy(gameObject);
    }

}
