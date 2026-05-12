using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] TextMeshProUGUI _buttonText;

    public void Init(LevelInfo levelInfo, int levelIndex, bool isUnlocked, Action<int> onClick)
    {
        if (_buttonText != null)
        {
            _buttonText.text = $"Уровень {levelIndex + 1}";
        }
        
        _button.interactable = isUnlocked;
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => onClick(levelIndex));
    }
}
