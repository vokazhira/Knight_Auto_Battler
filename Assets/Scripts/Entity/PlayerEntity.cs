using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    public int Armor {get; private set;}
    public int Exp  {get; private set;}
    
    private EnemyEntity _currentTarget;

    public override void Init(EntityData data)
    {
        if (data is PlayerData playerData)
        {
            base.Init(playerData);
            Armor = playerData.Armor;
            Exp = playerData.Exp;
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
        if (_currentTarget != null && _currentTarget.IsAlive())
        {
            //TODO: батлконтролер выдает цель
        }
    }

    public void AddExp(int amount)
    {
        Exp += amount;
    }
}
