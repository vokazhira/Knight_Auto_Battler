using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _maxHealthText;
    [SerializeField] private TextMeshProUGUI _armorText;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _attackDelayText;
    
    private BattleController _battleController;
    
    private void Start()
    {
        _battleController = BattleController.Instance;
        UpdateAllStats();
    }

    public void UpdateMaxHealth()
    {
        _maxHealthText.text = $"Здоровье: {_battleController.Player.MaxHealth}";
    }

    public void UpdateArmor()
    {
        _armorText.text = $"Защита: {_battleController.Player.Armor}";
    }

    public void UpdateDamage()
    {
        _damageText.text = $"Урон: {_battleController.Player.Damage}";
    }

    public void UpdateAttackDelay()
    {
        _attackDelayText.text = $"Скр. атаки: {_battleController.Player.AttackDelay}";
    }

    public void UpdateAllStats()
    {
        UpdateMaxHealth();
        UpdateArmor();
        UpdateDamage();
        UpdateAttackDelay();
    }
}
