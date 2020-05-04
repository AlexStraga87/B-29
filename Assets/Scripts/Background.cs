using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Transform _cameraTransform;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        _spriteRenderer.sprite = _sprites[SaveSystem.Instance.GetPlayerData().CurrentLevel];
    }

    private void Update()
    {
        transform.position = _cameraTransform.position / 15;
    }
}
