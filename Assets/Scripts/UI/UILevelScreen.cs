using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(PlayerDataLoader))]
public class UILevelScreen : MonoBehaviour
{
    [SerializeField] private RectTransform _circleMark;
    [SerializeField] private RectTransform[] _levelPoints;
    [TextArea]
    [SerializeField] private string[] _levelDecription;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private PlayerDataLoader _playerDataLoader;

    private void Start()
    {
        int currentLevel = _playerDataLoader.GetPlayerData().CurrentLevel;
        SetMarkAndDescription(currentLevel);
    }

    private void SetMarkAndDescription(int numLevel)
    {
        if (numLevel > _levelPoints.Length)
            return;
        _circleMark.anchoredPosition = _levelPoints[numLevel].anchoredPosition;
        _description.text = _levelDecription[numLevel];

    }


}
