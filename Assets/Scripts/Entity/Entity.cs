using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public int CurrentHealth { get; protected set; }
    public int MaxHealth { get; protected set; }
    public int Damage { get; protected set; }
    public float AttackDelay { get; protected set; }

    protected float _currentAttackTime;
    protected SpriteRenderer _spriteRenderer;
    
    public event Action<float> HealthChanged;

    protected virtual void Awake()
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
        CurrentHealth = MaxHealth;
        Damage = data.Damage;
        AttackDelay = data.AttackDelay;
        _spriteRenderer.sprite = data.Sprite;
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); 
            
        HealthChanged?.Invoke(1f);
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
        else
        {
            float currentHealthAsPercentage = (float) CurrentHealth / MaxHealth;
            HealthChanged?.Invoke(currentHealthAsPercentage);
        }
    }

    public abstract void Attack();
    public abstract void Die();
    
    public bool IsAlive() => CurrentHealth > 0;
}
