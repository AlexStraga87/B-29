﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerDataLoader))]
public class Background : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private PlayerDataLoader _playerDataLoader;
    private Transform _cameraTransform;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        _spriteRenderer.sprite = _sprites[_playerDataLoader.GetPlayerData().CurrentLevel];
    }

    private void Update()
    {
        transform.position = _cameraTransform.position / 15;
    }
}
