using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Events;

public class SaveSystem: MonoBehaviour
{
    static SaveSystem _instance;
    public static SaveSystem Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            if (_instance == null)
            {
                _instance = value;
            }
        }
    }
    public event UnityAction<int> MoneyChange;

    private PlayerData _playerData;
    private string path;

    private void Awake()
    {
        path = Application.persistentDataPath + "/Save.dat";
        Instance = this;
        LoadData();
    }

    public PlayerData GetPlayerData()
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
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = GetPlayerData();

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public void LoadData()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            _playerData = data;
        }
        else
        {
            _playerData = new PlayerData();
            _playerData.Initialize();
        }
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