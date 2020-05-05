using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int CurrentLevel;
    public int Money;
    public int[] Upgrades;

    public void Initialize()
    {
        CurrentLevel = 0;
        Money = 10000;
        Upgrades = new int[9];
        for (int i = 0; i < 9; i++)
        {
            Upgrades[i] = 0;
        }
        Upgrades[(int)UpgradesList.Plasma] = 1;
    }

}
