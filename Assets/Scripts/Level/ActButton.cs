using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _buttonText;

    public void Init(ActInfo actInfo, int actIndex, bool isUnlocked, Action<int> onClick)
    {
        if (_buttonText != null)
        {
            _buttonText.text = $"Акт {actIndex + 1}";
        }
        
        _button.interactable = isUnlocked;
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => onClick(actIndex));
    }
}
