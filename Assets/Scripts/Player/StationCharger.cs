using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationCharger : MonoBehaviour
{
    [SerializeField] Transform _field;
    [SerializeField] Player _player;
    private int _maxDistance = 5;
    private int _chargerCount = 8;

    private void Start()
    {
        PlayerData playerData = SaveSystem.Instance.GetPlayerData();
        Upgrade(playerData.Upgrades[(int)UpgradesList.PowerField]);
    }

    private void Update()
    {
        if (Vector2.Distance(_player.transform.position, transform.position) <= _maxDistance)
        {
            _player.AddEnergy(_chargerCount * Time.deltaTime);
        }
    }

    private void Upgrade(int level)
    {
        switch (level)
        {
            case 1:
                SetField(7, 1.9f, 9);
                break;                
            case 2:
                SetField(9, 2.4f, 10);
                break;
        }
    }

    private void SetField(int maxDistance, float scale, int chargerCount)
    {
        _maxDistance = maxDistance;
        _field.localScale = Vector3.one * scale;
        _chargerCount = chargerCount;
    }

}
