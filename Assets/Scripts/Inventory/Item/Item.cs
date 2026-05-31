using System;
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
    public int SalePrice { get; private set; }
    
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
        SalePrice = CalculateSalePrice();
    }
    
    private int CalculateSalePrice()
    {
        int rarityPrice = Rarity switch
        {
            ItemRarity.Common => 12,
            ItemRarity.Uncommon => 25,
            ItemRarity.Rare => 50,
            ItemRarity.Epic => 100,
            _ => 10
        };
        
        int statsValue = Health + Armor + Damage + (int)Mathf.Abs(AttackDelay) * 2;

        return rarityPrice + statsValue * 2;
    }
}