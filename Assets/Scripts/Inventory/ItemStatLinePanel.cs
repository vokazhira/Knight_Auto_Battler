using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemStatLinePanel : MonoBehaviour
{
    [SerializeField] private Image _statImage;
    [SerializeField] private TextMeshProUGUI _statValueText;
    /*[SerializeField] private TextMeshProUGUI _statTypeText;*/
    
    public void SetStatValueText(int value)
    {
        _statValueText.text = value.ToString();
    }
    /*public void SetStatTypeText(string typeText)
    {
        _statTypeText.text = typeText;
    }
    
    public void SetStatIcon(Sprite icon)
    {
        _statImage.sprite = icon;
    }*/
}
