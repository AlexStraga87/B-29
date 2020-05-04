using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossMoverWayPoint : BossMover
{
    public event UnityAction WayPoint;
    [SerializeField] private SuperLaserGun _laser;
    private bool _isPause = false;
    private bool _isLaserActive = false;
    private Coroutine _currentCoroutine;

    private void Start()
    {
        _minDistanceToTarget = 25;
        SetTarget(GameObject.Find("Station").transform);
    }

    private void OnEnable()
    {
        _laser.StopShooting += LaserStop;
    }

    private void OnDisable()
    {
        _laser.StopShooting -= LaserStop;
    }

    private void LaserStop()
    {
        _isLaserActive = false;
    }

    private void Update()
    {
        if (_isPause)
            return;
        if (IsMinDistanceToTarget() == false)
        {
            MoveToTarget();
        }
        else
        {
            MoveOrbitTarget(90);
        }

        if (transform.position.x > 0 && Mathf.Abs(transform.position.y) < 0.4f)
        {
            if (_currentCoroutine == null)
                _currentCoroutine = StartCoroutine(PauseOnWayPoint());
        }
    }

    private IEnumerator PauseOnWayPoint()
    {
        _isPause = true;
        _isLaserActive = true;
        WayPoint?.Invoke();
        yield return new WaitWhile (() => _isLaserActive);
        _isPause = false;
        yield return new WaitForSeconds(2.5f);
        _currentCoroutine = null;

    }
}
