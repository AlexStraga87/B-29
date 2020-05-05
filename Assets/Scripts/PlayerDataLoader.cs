using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerDataLoader : MonoBehaviour
{
    protected PlayerData _playerData;

    private void Awake()
    {
        LoadData();
    }

    protected void LoadData()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            string save = PlayerPrefs.GetString("save");
            _playerData = JsonConvert.DeserializeObject<PlayerData>(save);
        }
        else
        {
            _playerData = new PlayerData();
            _playerData.Initialize();

        }
    }

    public virtual PlayerData GetPlayerData()
    {
        return _playerData;
    }
}
