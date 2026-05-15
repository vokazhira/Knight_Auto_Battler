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
        }
    }

    public override void TakeDamage(int damage)
    {
        int damageToTake = damage - Armor;

        if (damageToTake < 0)
        {
            damageToTake = 0;
        }
        
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
}
