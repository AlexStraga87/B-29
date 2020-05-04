using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Destroyable
{
    public float EnergyCount => _energyCount;
    public event UnityAction<float> EnergyChange;
    public event UnityAction<float> HPChange;
    [SerializeField] private GameObject _effectByDead;
    private float _energyCount = 100;
    private int _energyMax = 100;

    private void Start()
    {
        Upgrades();
        _hp = _hpMax;        
    }

    protected void Upgrades()
    {
        PlayerData playerData = SaveSystem.Instance.GetPlayerData();
        _energyMax += playerData.Upgrades[(int)UpgradesList.Battery] * 15;
        _energyCount = _energyMax;

        int healthLevel = playerData.Upgrades[(int)UpgradesList.ShipHealth];
        if (healthLevel >= 1)
        {
            _hpMax += 10;
        }
        if (healthLevel == 2)
        {
            StartCoroutine(RegenerateCoroutine());
        }
    }

    protected IEnumerator RegenerateCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(2);
        while(true)
        {
            _hp++;
            if (_hp > _hpMax)
            {
                _hp = _hpMax;
            }
            HPChange?.Invoke(_hp / _hpMax);
            yield return wait;
        }
    }


    protected override void OnDead()
    {
        _effectByDead.SetActive(true);
    }

    protected override void OnTakeDamage(int damage)
    {
        if (_isImmortal) return;
        _energyCount -= damage;
        if (_energyCount < 0)
        {
            _hp += _energyCount;
            _energyCount = 0;
            HPChange?.Invoke(_hp / _hpMax);            
        }
        EnergyChange?.Invoke(_energyCount / _energyMax);
    }

    public void AddEnergy(float energyCount)
    {
        _energyCount += energyCount;
        if (_energyCount > _energyMax)
        {
            _energyCount = _energyMax;
        }
        EnergyChange?.Invoke(_energyCount / _energyMax);
    }

    public bool TryDecreaseEnergy(int energyCount)
    {
        if (_energyCount >= energyCount)
        {
            _energyCount -= energyCount;
            EnergyChange?.Invoke(_energyCount / _energyMax);
            return true;
        }
        return false;
    }


}
