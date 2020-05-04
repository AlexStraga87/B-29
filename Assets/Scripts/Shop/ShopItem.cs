using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public string Description => _description;
    public event UnityAction<UpgradesList> MouseClick;
    public event UnityAction<string> MouseEnter;
    public event UnityAction MouseExit;

    [SerializeField] private TMP_Text _upgradeLevelText;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private Image _icon;
    [SerializeField] private Sprite _iconSprite;
    [SerializeField] private UpgradesList _upgradesType;
    [TextArea]
    [SerializeField] private string _description;
    [SerializeField] private int[] _prices;
    private int _currentPrice;
    private int _currentLevel;

    private void Start()
    {
        _icon.sprite = _iconSprite;
        _currentLevel = SaveSystem.Instance.GetUpgradeLevel(_upgradesType);
        SetUpgradeLevel(_currentLevel);
    }

    public void SetUpgradeLevel(int value)
    {
        if (_prices.Length > value)
        {
            _upgradeLevelText.text = value.ToString();
            _costText.text = _prices[value].ToString();
            _currentPrice = _prices[value];
        }
        else
        {
            _upgradeLevelText.text = _prices.Length.ToString();
            _costText.text = "----";
            _currentPrice = 0;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseEnter?.Invoke(_description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseExit?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (SaveSystem.Instance.TryDecreaseMoney(_currentPrice))
        {
            MouseClick?.Invoke(_upgradesType);
            _currentLevel++;
            SetUpgradeLevel(_currentLevel);
            _audioSource.Play();
        }
    }
}
