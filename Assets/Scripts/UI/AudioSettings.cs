using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private string _soundParameterName;
    [SerializeField] private string _musicParameterName;


    private void OnEnable()
    {
        _soundSlider.value = GetVolume(_soundParameterName);
        _musicSlider.value = GetVolume(_musicParameterName);
        _soundSlider.onValueChanged.AddListener(OnSoundVolume);
        _musicSlider.onValueChanged.AddListener(OnMusicVolume);
    }

    private void OnDisable()
    {
        _soundSlider.onValueChanged.RemoveListener(OnSoundVolume);
        _musicSlider.onValueChanged.RemoveListener(OnMusicVolume);
    }

    private float GetVolume(string parameterName)
    {
        float volume;
        _audioMixer.GetFloat(parameterName, out volume);
        return volume;
    }

    private void SetVolume(string parameterName, float volume)
    {
        if (volume <= -28)
            volume = -80;

        _audioMixer.SetFloat(parameterName, volume);
    }

    private void OnSoundVolume(float volume)
    {
        SetVolume(_soundParameterName, volume);
    }

    private void OnMusicVolume(float volume)
    {
        SetVolume(_musicParameterName, volume);
    }


}
