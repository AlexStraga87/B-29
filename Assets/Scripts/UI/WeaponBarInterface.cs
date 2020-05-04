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
        _weaponScript.Activate += Activate;
        _weaponScript.Reload += ReloadTime;
    }

    private void OnDisable()
    {
        _weaponScript.Activate -= Activate;
        _weaponScript.Reload -= ReloadTime;
    }

    private void Activate(bool isActive)
    {
        _weaponImage.enabled = isActive;
    }

    private void ReloadTime(float reloadTimeNormalize)
    {
        _weaponImage.fillAmount = reloadTimeNormalize;
    }

}

