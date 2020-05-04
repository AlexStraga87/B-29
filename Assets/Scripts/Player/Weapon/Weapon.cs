using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public abstract class Weapon : MonoBehaviour
{
    public int ShootCost => _shootCost;
    public bool IsUnlocked => _isUnlocked;
    public event UnityAction<float> Reload;
    public event UnityAction<bool> Activate;
    [SerializeField] public bool isRealoaded => _lastFireTime <= 0;
    [SerializeField] public string Label { get; private set; }
    [SerializeField] protected int _shootCost = 2;  
    [SerializeField] protected GameObject _bulletTemplate;
    [SerializeField] protected int _damage;
    [SerializeField] protected float _fireReloadTime = 2;
    [SerializeField] protected AudioSource _audioSource;
    [SerializeField] protected AudioClip _shootEffect;

    protected bool _isUnlocked = true;
    protected int _rageMultiply = 1;

    protected float _lastFireTime = 0;

    protected void Reloading()
    {
        _lastFireTime -= Time.deltaTime;
        if (_lastFireTime < 0) _lastFireTime = 0;
        Reload?.Invoke(1 - _lastFireTime / _fireReloadTime);
    }

    protected void ResetReloadingTime()
    {
        _lastFireTime = _fireReloadTime;
        Reload?.Invoke(1 - _lastFireTime / _fireReloadTime);
    }

    protected virtual void ConfigurateBullet(Bullet bullet)
    {
    }

    public void Fire()
    {
        Bullet bullet = Instantiate(_bulletTemplate, transform.position + transform.up * 1.5f, transform.rotation).GetComponent<Bullet>();
        bullet.Initilize(transform.up, _damage * _rageMultiply);
        ResetReloadingTime();
        ConfigurateBullet(bullet);
        _audioSource.PlayOneShot(_shootEffect);
    }

    public void StartRageMode()
    {
        _rageMultiply = 2;
    }

    public void StopRageMode()
    {
        _rageMultiply = 1;
    }

    public abstract void Upgrade(int level);

    private void OnEnable()
    {
        Activate?.Invoke(true);
    }

    private void OnDisable()
    {
        Activate?.Invoke(false);
    }


}
