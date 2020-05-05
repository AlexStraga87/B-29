using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAsteroids : MonoBehaviour
{
    [SerializeField] Asteroid _template;
    [SerializeField] Sprite[] _asteroidsVariant;
    
    private void Start()
    {
        for (int i = 0; i < 35; i++)
        {
            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid()
    {
        var newAsteroid = Instantiate(_template, GetRandomPosition(), Quaternion.Euler(0, 0, Random.Range(0, 360)), transform);
        newAsteroid.GetComponent<SpriteRenderer>().sprite = _asteroidsVariant[Random.Range(0, _asteroidsVariant.Length)];
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 position = new Vector3();
        do
        {
            position.x = Random.Range(-70, 70);
            position.y = Random.Range(-70, 60);
        } while (Mathf.Abs(position.x) < 20 && Mathf.Abs(position.y) < 20);
        return position;
    }
}
