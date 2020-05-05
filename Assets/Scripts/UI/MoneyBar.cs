using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyBar : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private SaveSystem _saveSystem;

    private void OnEnable()
    {
        _saveSystem.MoneyChange += OnMoneyTextChange;        
    }

    private void OnDisable()
    {
        _saveSystem.MoneyChange -= OnMoneyTextChange;
    }

    private void Start()
    {
        _saveSystem.AddMoney(0);
    }

    private void OnMoneyTextChange(int value)
    {
        _moneyText.text = value.ToString();
    }
}

