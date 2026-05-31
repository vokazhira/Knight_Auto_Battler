using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    [SerializeField] private TextMeshProUGUI _currencyText;
    
    public int CurrencyAmount { get; private set; } = 100;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        UpdateUI();
    }

    public void AddCurrency(int amount)
    {
        if (amount <= 0) return;
        CurrencyAmount += amount;
        UpdateUI();
        EventManager.Invoke(GameEvents.CurrencyChanged, CurrencyAmount);
    }
    
    public bool SpendCurrency(int amount)
    {
        if (CurrencyAmount >= amount)
        {
            CurrencyAmount -= amount;
            UpdateUI();
            EventManager.Invoke(GameEvents.CurrencyChanged, CurrencyAmount);
            return true;
        }
        return false;
    }
    
    private void UpdateUI()
    {
        _currencyText.text = $"{CurrencyAmount}";
    }
}
