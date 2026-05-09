using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public int Health { get; protected set; }
    public int MaxHealth { get; protected set; }
    public int Damage { get; protected set; }
    public float AttackDelay { get; protected set; }

    protected float _currentAttackTime;
    protected SpriteRenderer _spriteRenderer;

    protected virtual void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        if (_currentAttackTime < AttackDelay)
        {
            _currentAttackTime += Time.deltaTime;   
        }
        else
        {
            Attack();
            _currentAttackTime = 0f;
        }
    }

    public virtual void Init(EntityData data)
    {
        MaxHealth = data.Health;
        Health = MaxHealth;
        Damage = data.Damage;
        AttackDelay = data.AttackDelay;
        _spriteRenderer.sprite = data.Sprite;
    }

    public abstract void Attack();

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
        else
        {
            //TODO: логика хелфбара
        }
    }

    public abstract void Die();
    
    public bool IsAlive() => Health > 0;
}
