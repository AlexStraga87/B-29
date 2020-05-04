using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResolutionSettings : MonoBehaviour
{
    [SerializeField] private TMP_Text _textOutputResolution;
    [SerializeField] private Toggle _toggleFullScreen;
    private List<Resolution> _resolutions = new List<Resolution>(); 
    private int _currentResolutionIndex = 0;
    private int _newResolutionIndex = 0;

    private void Awake()
    {
        Resolution[] resolutions = Screen.resolutions;
        foreach (var resolution in resolutions)
        {
            _resolutions.Add(resolution);
        }

        for (int i = 0; i < _resolutions.Count - 1; i++)
        {
            if (_resolutions[i].width == _resolutions[i + 1].width && _resolutions[i].height == _resolutions[i + 1].height)
            {
                _resolutions.RemoveAt(i + 1);
                i--;
            }
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < _resolutions.Count; i++)
        {
            if (_resolutions[i].width == Screen.width && _resolutions[i].height == Screen.height)
            {
                _currentResolutionIndex = i;
                _newResolutionIndex = i;
                ShowResolution(i);
                break;
            }
        }
        _toggleFullScreen.isOn = Screen.fullScreen;
    }

    private void ShowResolution(int index)
    {
        _textOutputResolution.text = _resolutions[index].width + "x" + _resolutions[index].height;        
    }

    public void ApplyResolution()
    {
        Screen.SetResolution(_resolutions[_newResolutionIndex].width, _resolutions[_newResolutionIndex].height, _toggleFullScreen.isOn);
    }

    public void NextResolution()
    {
        if (_newResolutionIndex < _resolutions.Count - 1)
        {
            _newResolutionIndex++;
            ShowResolution(_newResolutionIndex);
        }
    }

    public void PrevResolution()
    {
        if (_newResolutionIndex > 0)
        {
            _newResolutionIndex--;
            ShowResolution(_newResolutionIndex);
        }
    }
}
