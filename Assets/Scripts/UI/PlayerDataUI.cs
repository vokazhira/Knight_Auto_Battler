using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _maxHealthText;
    [SerializeField] private TextMeshProUGUI _armorText;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _attackDelayText;

    private GameManager _gameManager;
    //нужен не плеерДата, а плеерЕнтити
    
    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void UpdateMaxHealth()
    {
        _maxHealthText.text = $"Здоровье: {_gameManager.PlayerData.Health}";
    }

    public void UpdateArmor()
    {
        _armorText.text = $"Защита: {_gameManager.PlayerData.Armor}";
    }

    public void UpdateDamage()
    {
        _damageText.text = $"Урон: {_gameManager.PlayerData.Damage}";
    }

    public void UpdateAttackDelay()
    {
        _attackDelayText.text = $"Скр. атаки: {_gameManager.PlayerData.AttackDelay}";
    }

    public void UpdateAllStats()
    {
        UpdateMaxHealth();
        UpdateArmor();
        UpdateDamage();
        UpdateAttackDelay();
    }
}
