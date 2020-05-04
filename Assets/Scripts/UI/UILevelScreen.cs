using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILevelScreen : MonoBehaviour
{
    [SerializeField] private RectTransform _circleMark;
    [SerializeField] private RectTransform[] _levelPoints;
    [TextArea]
    [SerializeField] private string[] _levelDecription;
    [SerializeField] private TMP_Text _description;

    private void Start()
    {
        int currentLevel = SaveSystem.Instance.GetPlayerData().CurrentLevel;
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
