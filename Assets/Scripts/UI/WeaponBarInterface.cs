using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponBarInterface : MonoBehaviour
{
    [SerializeField] private Image _weaponImage;
    [SerializeField] private Weapon _weaponScript;

    private void OnEnable()
    {
        _weaponScript.Activate += OnActivate;
        _weaponScript.Reload += OnReloadTime;
    }

    private void OnDisable()
    {
        _weaponScript.Activate -= OnActivate;
        _weaponScript.Reload -= OnReloadTime;
    }

    private void OnActivate(bool isActive)
    {
        _weaponImage.enabled = isActive;
    }

    private void OnReloadTime(float reloadTimeNormalize)
    {
        _weaponImage.fillAmount = reloadTimeNormalize;
    }

}

