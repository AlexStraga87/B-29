using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatChecker : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Station _station;
    [SerializeField] private GameObject _defeatMenu;
    [SerializeField] private AudioSource _musicSource;
    private void OnEnable()
    {
        _player.HPChange += Checker;
        _station.HPChange += Checker;
    }

    private void OnDisable()
    {
        _player.HPChange -= Checker;
        _station.HPChange -= Checker;
    }

    private void Checker(float value)
    {
        if (value <= 0)
        {
            StartCoroutine(DefeatCoroutine());
        }
    }

    protected IEnumerator DefeatCoroutine()
    {
         yield return new WaitForSeconds(2);
        _defeatMenu.SetActive(true);
        _musicSource.Stop();
        Time.timeScale = 0;
    }
}
