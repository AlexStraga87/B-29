using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerDataLoader))]
public class Station : Destroyable
{
    [SerializeField] private PlayerDataLoader _playerDataLoader;
    [SerializeField] private float _chargerCount = 3;
    [SerializeField] private GameObject _effectByDead;
    private float _energyCount = 0;
    private float _energyMax = 0;

    public event UnityAction<float> EnergyChange;
    public event UnityAction<float> HPChange;

    private void Start()
    {
        Upgrades();
        _hp = _hpMax;
    }

    private void Update()
    {
        AddEnergy(_chargerCount * Time.deltaTime);
    }

    public void AddEnergy(float value)
    {
        _energyCount += value;
        if (_energyCount > _energyMax)
        {
            _energyCount = _energyMax;
        }
        EnergyChange?.Invoke(_energyCount / Mathf.Max(_energyMax, 1));
    }

    protected void Upgrades()
    {
        PlayerData playerData = _playerDataLoader.GetPlayerData();
        _energyMax += playerData.Upgrades[(int)UpgradesList.StationShield] * 15;
        _energyCount = _energyMax;
    }

    protected override void OnTakeDamage(int damage)
    {
        if (_isImmortal) return;
        _energyCount -= damage;
        if (_energyCount < 0)
        {
            _hp += _energyCount;
            _energyCount = 0;

            HPChange?.Invoke(_hp / Mathf.Max(_hpMax, 1f));
        }
        EnergyChange?.Invoke(_energyCount/Mathf.Max(_energyMax, 1));
    }

    protected override void OnDead()
    {

        _effectByDead.SetActive(true);
    }
}
