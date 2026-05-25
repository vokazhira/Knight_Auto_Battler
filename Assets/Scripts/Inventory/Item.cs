using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item
{
    public int Health {get; private set;}
    public int Armor {get; private set;}
    public int Damage {get; private set;}
    public int AttackDelay {get; private set;}
    
    public int GetHealthBonus() => Health;
    public int GetArmorBonus() => Armor;
    public int GetDamageBonus() => Damage;
    public int GetAttackDelayBonus() => AttackDelay;
    
    public Sprite Sprite {get; private set;}
    public ItemType Type {get; private set;}
    public ItemRarity Rarity {get; private set;}

    public Item(int health, int armor, int damage, int attackDelay, Sprite sprite, ItemType type, ItemRarity rarity)
    {
        Health = health;
        Armor = armor;
        Damage = damage;
        AttackDelay = attackDelay;
        Sprite = sprite;
        Type = type;
        Rarity = rarity;
    }
}