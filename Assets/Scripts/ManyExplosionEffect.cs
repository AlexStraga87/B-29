using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManyExplosionEffect : MonoBehaviour
{
    [SerializeField] GameObject _template;
    [SerializeField] float _radius = 1;
    [SerializeField] float _scale = 1;
    [SerializeField] float _count = 5;

    private void Start()
    {
        StartCoroutine(CreateEffectsCoroutine());
    }

    private IEnumerator CreateEffectsCoroutine()
    {
        for (int i = 0; i < _count; i++)
        {
            GameObject boom = Instantiate(_template, transform.position, Quaternion.Euler(0,0,Random.Range(0, 360)));
            Vector3 scale = Vector3.one * _scale;
            boom.transform.localScale = scale;
            boom.transform.position += (Vector3)Random.insideUnitCircle * _radius;

            yield return new WaitForSeconds(0.3f);
        }
        Destroy(gameObject, 5);
    }
}
