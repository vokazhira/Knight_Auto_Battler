using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    private int _armor;
    public int Armor => _armor;
    
    private int _currentExp;
    public int CurrentExp => _currentExp;

    private int _maxExp = 100;
    public int MaxExp => _maxExp;
    
    private int _currentLevel = 1;
    public int CurrentLevel => _currentLevel;
    
    private int _statsPointCount = 0;
    public int StatsPointCount => _statsPointCount;
    
    private PlayerStatsData _statsData = new PlayerStatsData();
    
    private EnemyEntity _currentTarget;
    
    private void OnEnable()
    {
        EventManager.Subscribe(GameEvents.StatUpgradeRequested, OnStatUpgradeRequested);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(GameEvents.StatUpgradeRequested, OnStatUpgradeRequested);
    }
    
    public override void Init(EntityData data)
    {
        GameManager.Instance.SetPlayer(this);
        
        if (data is PlayerData playerData)
        {
            base.Init(playerData);
            _armor = playerData.Armor;
            _currentExp = playerData.Exp;
            
            EventManager.Subscribe(GameEvents.EnemyDeath, AddExp);
            EventManager.Subscribe(GameEvents.PlayerDied, ResetHealth);
            EventManager.Subscribe(GameEvents.ApplyEquipmentBonus, OnEquipmentBonusChanged);
            EventManager.Subscribe(GameEvents.StatPointUsed, OnStatPointUsed);

            BroadcastStats();
        }
    }

    public override void TakeDamage(int damage)
    {
        int damageToTake = damage - Armor;

        if (damageToTake < 0) damageToTake = 1;
        
        base.TakeDamage(damageToTake);
    }

    public override void Attack()
    {
        if (_currentTarget == null || !_currentTarget.IsAlive())
        {
            _currentTarget = BattleController.GetRandomAliveEnemy();
        }
        
        if (_currentTarget != null && _currentTarget.IsAlive())
        {
            _currentTarget.TakeDamage(Damage);
        }
    }

    public void AddExp(object expObj)
    {
        if (expObj is int exp)
        {
            _currentExp += exp;

            while (_currentExp >= _maxExp)
            {
                _currentLevel++;
                _currentExp -= _maxExp;
                _maxExp += 100 * _currentLevel;
                _statsPointCount++;

                BroadcastStats();
            }
            
            BroadcastStats();
        }
    }

    public void UseStatPooint()
    {
        if (_statsPointCount > 0)
        {
            _statsPointCount--;
            BroadcastStats();
        }
    }

    public void OnStatPointUsed(object obj)
    {
        _statsPointCount--;
    }

    public void UpMaxHealth()
    {
        _maxHealth += 100 * _currentLevel / 2;
    }

    public void UpArmor()
    {
        _armor += 4 * _currentLevel / 2;
    }

    public void UpDamage()
    {
        _damage += 10 * _currentLevel / 2;;
    }

    public void UpAttackDelay()
    {
        _attackDelay += 1 * _currentLevel / 2;;
    }
    
    public override void Die()
    {
        EventManager.Invoke(GameEvents.PlayerDied);
    }

    private void BroadcastStats()
    {
        _statsData.MaxHealth = _maxHealth;
        _statsData.Armor = _armor;
        _statsData.Damage = _damage;
        _statsData.AttackDelay = _attackDelay;
        _statsData.StatPoints = _statsPointCount;
        _statsData.CurrentLevel = _currentLevel;

        EventManager.Invoke(GameEvents.PlayerStatsChanged, _statsData);
    }
    
    private void OnStatUpgradeRequested(object obj)
    {
        if (obj is StatType statType && _statsPointCount > 0)
        {
            switch (statType)
            {
                case StatType.Health: UpMaxHealth(); break;
                case StatType.Armor: UpArmor(); break;
                case StatType.Damage: UpDamage(); break;
                case StatType.AttackDelay: UpAttackDelay(); break;
            }
            
            _statsPointCount--;
            
            BroadcastStats();
            EventManager.Invoke(GameEvents.PlayerLevelUp, _statsPointCount);
        }
    }

    private void OnEquipmentBonusChanged(object obj)
    {
        if (obj is EquipmentBonusData equipmentBonusData)
        {
            int sign;
            if (equipmentBonusData.IsEquip) sign = 1;
            else sign = -1;

            _maxHealth += equipmentBonusData.Item.Health * sign;
            _armor += equipmentBonusData.Item.Armor * sign;
            _damage += equipmentBonusData.Item.Damage * sign;

            if (equipmentBonusData.Item.AttackDelay != 0)
            {
                _attackDelay += equipmentBonusData.Item.AttackDelay * sign;
            }
            
            EventManager.Invoke(GameEvents.PlayerStatsChanged);
            BroadcastStats();
        }
    }
}
