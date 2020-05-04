using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _sprites;
    private int _currentFrame = 0;

    public void NextFrame()
    {
        if (_currentFrame < _sprites.Length - 1)
        {
            _currentFrame++;
            _image.sprite = _sprites[_currentFrame];
        }
    }

    public void PrevFrame()
    {
        if (_currentFrame > 0)
        {
            _currentFrame--;
            _image.sprite = _sprites[_currentFrame];
        }
    }
}
