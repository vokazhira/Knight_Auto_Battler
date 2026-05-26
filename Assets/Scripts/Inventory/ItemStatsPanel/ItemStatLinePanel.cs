using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemStatLinePanel : MonoBehaviour
{
    [SerializeField] private Image _statImage;
    [SerializeField] private TextMeshProUGUI _statValueText;
    
    public void SetStatValueText(int value)
    {
        _statValueText.text = value.ToString();
    }
}
