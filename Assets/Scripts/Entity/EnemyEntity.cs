using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class EnemyEntity : Entity
{
    public int ExpReward {get; private set;}

    private PlayerEntity _player;
    
    public override void Init(EntityData data)
    {
        if (data is EnemyData enemyData)
        {
            base.Init(enemyData);
            ExpReward = enemyData.ExpReward;
        }
    }
    
    public void SetPlayerAsTarget(PlayerEntity player)
    {
        _player = player;
    }

    public override void Attack()
    {
        _player.TakeDamage(Damage);
    }

    public override void Die()
    {
        EventManager.Invoke(GameEvents.EnemyDeath, ExpReward);
        LeanPool.Despawn(this.gameObject);
    }
}
