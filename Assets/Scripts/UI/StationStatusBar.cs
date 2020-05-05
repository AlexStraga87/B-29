using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StationStatusBar : MonoBehaviour
{
    [SerializeField] Slider _sliderStationHP;
    [SerializeField] Slider _sliderStationEnergy;
    [SerializeField] Station _station;

    private void OnEnable()
    {
        _station.HPChange += OnSliderStationHPChange;
        _station.EnergyChange += OnSliderStationEnergyChange;
    }

    private void OnDisable()
    {
        _station.HPChange -= OnSliderStationHPChange;
        _station.EnergyChange -= OnSliderStationEnergyChange;
    }

    private void OnSliderStationHPChange(float value)
    {
        _sliderStationHP.value = value;
    }

    private void OnSliderStationEnergyChange(float value)
    {
        _sliderStationEnergy.value = value;
    }
}

