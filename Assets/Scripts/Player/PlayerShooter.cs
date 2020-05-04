using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerShooter : MonoBehaviour
{
    [SerializeField] List<Weapon> _weapons;
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        PlayerData playerData = SaveSystem.Instance.GetPlayerData();
        _weapons[0].Upgrade(playerData.Upgrades[(int)UpgradesList.Plasma]);
        _weapons[1].Upgrade(playerData.Upgrades[(int)UpgradesList.Laser]);
        _weapons[2].Upgrade(playerData.Upgrades[(int)UpgradesList.Rocket]);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            for (int i = 0; i < 3; i++)
            {
                if (_weapons[i].enabled && _weapons[i].IsUnlocked && _weapons[i].isRealoaded)
                {
                    if (player.TryDecreaseEnergy(_weapons[i].ShootCost)) _weapons[i].Fire();
                }
            }
        }


        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            WeaponSwitcher(0);
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            WeaponSwitcher(1);
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            WeaponSwitcher(2);
        }

    }

    private void WeaponSwitcher(int slot)
    {
        if (_weapons[slot].IsUnlocked)
            _weapons[slot].enabled = !_weapons[slot].enabled;
    }
}
