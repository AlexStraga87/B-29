using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Newtonsoft.Json;

public class SaveSystem: PlayerDataLoader
{
    public event UnityAction<int> MoneyChange;

    private void Awake()
    {
        LoadData();
    }

    public override PlayerData GetPlayerData()
    {
        if (_playerData == null)
        {
            _playerData = new PlayerData();
            _playerData.Initialize();
            SaveData();
        }
        return _playerData;
    }

    public void ResetSaveData()
    {
        _playerData = new PlayerData();
        _playerData.Initialize();
        SaveData();
    }

    public void PassLevel()
    {
        _playerData.CurrentLevel++;
    }

    public bool TryDecreaseMoney(int value)
    {
        if (_playerData.Money >= value)
        {
            _playerData.Money -= value;
            SaveData();
            MoneyChange?.Invoke(_playerData.Money);
            return true;
        }
        return false;
    }

    public void AddMoney(int value)
    {
        _playerData.Money += value;
        MoneyChange?.Invoke(_playerData.Money);
    }

    public void SaveData()
    {
        string save = JsonConvert.SerializeObject(_playerData);
        PlayerPrefs.SetString("save", save);
        PlayerPrefs.Save();
    }

    public int GetUpgradeLevel(UpgradesList upgrade)
    {
        return _playerData.Upgrades[(int)upgrade];
    }

    public void Upgrade(UpgradesList upgrade)
    {
        _playerData.Upgrades[(int)upgrade]++;
        SaveData();
    }
}

[System.Serializable]
public enum UpgradesList {StationShield, PowerField, StationGun, Plasma, Laser, Rocket, ShipSpeed, ShipHealth, Battery};