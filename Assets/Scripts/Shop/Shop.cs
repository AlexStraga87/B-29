using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _moneytext;
    [SerializeField] private SaveSystem _saveSystem;

    private ShopItem[] _shopItems;

    private void Awake()
    {
        _shopItems = GetComponentsInChildren<ShopItem>();
    }

    private void OnEnable()
    {
        foreach (var item in _shopItems)
        {
            item.MouseEnter += ChangeDescription;
            item.MouseExit += ClearDescription;
            item.MouseClick += Buy;
        }
        _saveSystem.MoneyChange += ShowMoney;
        ShowMoney(_saveSystem.GetPlayerData().Money);
    }

    private void OnDisable()
    {
        foreach (var item in _shopItems)
        {
            item.MouseEnter -= ChangeDescription;
            item.MouseExit -= ClearDescription;
            item.MouseClick -= Buy;
        }
        _saveSystem.MoneyChange -= ShowMoney;
    }

    private void ClearDescription()
    {
        _description.text = "";
    }

    private void ChangeDescription(string text)
    {
        _description.text = text;
    }

    private void Buy(UpgradesList shopItem)
    {
        _saveSystem.Upgrade(shopItem);
    }

    private void ShowMoney(int money)
    {
        _moneytext.text = money.ToString();
    }


}
