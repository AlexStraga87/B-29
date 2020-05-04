using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Station _station;
    [SerializeField] private Camera _camera;

    private bool _isStationDead;
    private Vector3 _offset = new Vector3(0, 0, -20);

    private void OnEnable()
    {
        _station.HPChange += CheckStation;
    }

    private void OnDisable()
    {
        _station.HPChange -= CheckStation; 
    }

    private void Update()
    {
        transform.position = _target.position + _offset;
        _camera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * 4f;
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, 7.5f, 20);
    }

    private void CheckStation(float hp)
    {
        if (hp <= 0)
            _target = _station.transform;
    }


}
