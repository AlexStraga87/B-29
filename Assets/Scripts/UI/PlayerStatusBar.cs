using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatusBar : MonoBehaviour
{
    [SerializeField] Slider _sliderPlayerHP;
    [SerializeField] Slider _sliderPlayerEnergy;
    [SerializeField] Player _player;

    private void OnEnable()
    {
        _player.HPChange += OnSliderPlayerHPChange;
        _player.EnergyChange += OnSliderPlayerEnergyChange;
    }

    private void OnDisable()
    {
        _player.HPChange -= OnSliderPlayerHPChange;
        _player.EnergyChange -= OnSliderPlayerEnergyChange;
    }

    private void OnSliderPlayerHPChange(float value)
    {
        _sliderPlayerHP.value = value;
    }

    private void OnSliderPlayerEnergyChange(float value)
    {
        _sliderPlayerEnergy.value = value;
    }
}

