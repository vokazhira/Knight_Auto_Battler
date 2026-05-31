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
    [SerializeField] private TextMeshProUGUI _statPointsText;
    
    [SerializeField] private Button _upHealthButton;
    [SerializeField] private Button _upArmorButton;
    [SerializeField] private Button _upDamageButton;
    [SerializeField] private Button _upAttackDelayButton;
    
    /*private BattleController _battleController;*/
    
    private void OnEnable()
    {
        EventManager.Subscribe(GameEvents.PlayerStatsChanged, OnStatsChanged);
        EventManager.Subscribe(GameEvents.PlayerLevelUp, OnPlayerLevelUp);
        
        _upHealthButton.onClick.AddListener(() => RequestStatUpgrade(StatType.Health));
        _upArmorButton.onClick.AddListener(() => RequestStatUpgrade(StatType.Armor));
        _upDamageButton.onClick.AddListener(() => RequestStatUpgrade(StatType.Damage));
        _upAttackDelayButton.onClick.AddListener(() => RequestStatUpgrade(StatType.AttackDelay));
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(GameEvents.PlayerStatsChanged, OnStatsChanged);
        EventManager.Unsubscribe(GameEvents.PlayerLevelUp, OnPlayerLevelUp);
        
        _upHealthButton.onClick.RemoveAllListeners();
        _upArmorButton.onClick.RemoveAllListeners();
        _upDamageButton.onClick.RemoveAllListeners();
        _upAttackDelayButton.onClick.RemoveAllListeners();
    }
    
    private void Start()
    {
        
    }
    
    private void RequestStatUpgrade(StatType statType)
    {
        EventManager.Invoke(GameEvents.StatUpgradeRequested, statType);
    }
    
    private void OnStatsChanged(object obj)
    {
        if (obj is PlayerStatsData data)
        {
            UpdateUI(data);
        }
    }

    private void OnPlayerLevelUp(object obj)
    {
        if (obj is int statPointCount)
        {
            if (statPointCount > 0)
            {
                
            }
        }
    }
    
    private void UpdateUI(PlayerStatsData data)
    {
        _maxHealthText.text = $"Здр : {data.MaxHealth}";
        _armorText.text = $"Брн : {data.Armor}";
        _damageText.text = $"Урн : {data.Damage}";
        _attackDelayText.text = $"Скр : {data.AttackDelay}";
        _statPointsText.text = $"{data.StatPoints}";
    }
}
