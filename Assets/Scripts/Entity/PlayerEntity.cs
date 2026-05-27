using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    public int Armor {get; private set;}
    public int Exp {get; private set;}
    
    private EnemyEntity _currentTarget;
    
    public override void Init(EntityData data)
    {
        if (data is PlayerData playerData)
        {
            base.Init(playerData);
            Armor = playerData.Armor;
            Exp = playerData.Exp;
            
            EventManager.Subscribe(GameEvents.EnemyDeath, AddExp);
            EventManager.Subscribe(GameEvents.PlayerDied, ResetHealth);
            EventManager.Subscribe(GameEvents.ApplyEquipmentBonus, OnEquipmentBonusChanged);
            
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

    public void AddExp(object exp)
    {
        if (exp is int expInt)
        {
            Exp += expInt;
        }
    }
    
    public override void Die()
    {
        EventManager.Invoke(GameEvents.PlayerDied);
    }

    private void OnEquipmentBonusChanged(object obj)
    {
        if (obj is EquipmentBonusData equipmentBonusData)
        {
            int sign;
            if (equipmentBonusData.IsEquip) sign = 1;
            else sign = -1;

            MaxHealth += equipmentBonusData.Item.Health * sign;
            Armor += equipmentBonusData.Item.Armor * sign;
            Damage += equipmentBonusData.Item.Damage * sign;

            if (equipmentBonusData.Item.AttackDelay != 0)
            {
                AttackDelay += equipmentBonusData.Item.AttackDelay * sign;
            }
            
            EventManager.Invoke(GameEvents.PlayerStatsChanged);
        }
    }
}
