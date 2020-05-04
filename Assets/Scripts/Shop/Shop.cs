using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _moneytext;

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
        SaveSystem.Instance.MoneyChange += ShowMoney;
        ShowMoney(SaveSystem.Instance.GetPlayerData().Money);
    }

    private void OnDisable()
    {
        foreach (var item in _shopItems)
        {
            item.MouseEnter -= ChangeDescription;
            item.MouseExit -= ClearDescription;
            item.MouseClick -= Buy;
        }
        SaveSystem.Instance.MoneyChange -= ShowMoney;
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
        SaveSystem.Instance.Upgrade(shopItem);
    }

    private void ShowMoney(int money)
    {
        _moneytext.text = money.ToString();
    }


}
