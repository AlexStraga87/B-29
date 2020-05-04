using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusBarInterface : MonoBehaviour
{
    [SerializeField] Slider _sliderStationHP;
    [SerializeField] Slider _sliderStationEnergy;
    [SerializeField] Slider _sliderPlayerHP;
    [SerializeField] Slider _sliderPlayerEnergy;
    [SerializeField] Slider _sliderProgress;
    [SerializeField] TMP_Text _moneyText;

    [SerializeField] Player _player;
    [SerializeField] Station _station;
    [SerializeField] LevelSpawner _levelSpawner;

    private void OnEnable()
    {
        _player.HPChange += SliderPlayerHPChange;
        _player.EnergyChange += SliderPlayerEnergyChange;
        _station.HPChange += SliderStationHPChange;
        _station.EnergyChange += SliderStationEnergyChange;
        _levelSpawner.TimePassed += SliderProgressChange;
        SaveSystem.Instance.MoneyChange += MoneyTextChange;
        SaveSystem.Instance.AddMoney(0);
    }

    private void OnDisable()
    {
        _player.HPChange -= SliderPlayerHPChange;
        _player.EnergyChange -= SliderPlayerEnergyChange;
        _station.HPChange -= SliderStationHPChange;
        _station.EnergyChange -= SliderStationEnergyChange;
        _levelSpawner.TimePassed -= SliderProgressChange;
        SaveSystem.Instance.MoneyChange -= MoneyTextChange;
    }

    private void SliderPlayerHPChange(float value)
    {
        _sliderPlayerHP.value = value;
    }

    private void SliderPlayerEnergyChange(float value)
    {
        _sliderPlayerEnergy.value = value;
    }

    private void SliderStationHPChange(float value)
    {
        _sliderStationHP.value = value;
    }

    private void SliderStationEnergyChange(float value)
    {
        _sliderStationEnergy.value = value;
    }

    private void MoneyTextChange(int value)
    {
        _moneyText.text = value.ToString();
    }

    private void SliderProgressChange(float value)
    {
        _sliderProgress.value = value;
    }
}

