using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressLevelBar : MonoBehaviour
{
    [SerializeField] Slider _sliderProgress;
    [SerializeField] LevelSpawner _levelSpawner;

    private void OnEnable()
    {
        _levelSpawner.TimePassed += OnSliderProgressChange;
    }

    private void OnDisable()
    {
        _levelSpawner.TimePassed -= OnSliderProgressChange;
    }
    
    private void OnSliderProgressChange(float value)
    {
        _sliderProgress.value = value;
    }
}

