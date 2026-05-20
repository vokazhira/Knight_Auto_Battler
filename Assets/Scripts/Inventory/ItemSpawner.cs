using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private ItemData _itemData;
    
    [SerializeField][Range(0f, 1f)] private float _dropChance = 0.2f;
    
    [SerializeField] private int _minHealth = 5;
    [SerializeField] private int _maxHealth = 50;
    [SerializeField] private int _minArmor = 1;
    [SerializeField] private int _maxArmor = 10;
    [SerializeField] private int _minDamage = 1;
    [SerializeField] private int _maxDamage = 15;
    [SerializeField] private int _minAttackDelay = 1;
    [SerializeField] private int _maxAttackDelay = 5;

    private void OnEnable()
    {
        EventManager.Subscribe(GameEvents.EnemyDeath, OnEnemyDeath);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(GameEvents.EnemyDeath, OnEnemyDeath);
    }

    private void OnEnemyDeath(object obj)
    {
        float randomValue = Random.Range(0f, 1f);

        if (randomValue <= _dropChance)
        {
            Item newItem = GenerateRandomItem();
            _inventory.AddItem(newItem);
        }
    }

    private Item GenerateRandomItem()
    {
        ItemType[] allTypes = new[]
        {
            ItemType.Helmet, ItemType.Shield, ItemType.Sword,
            ItemType.Armor, ItemType.Boots, ItemType.Ring, ItemType.Necklace
        };
        
        ItemType itemType = allTypes[Random.Range(0, allTypes.Length)];
        ItemRarity itemRarity = GetRandomItemRarity();
        Sprite itemIcon = GetIconForItemByType(itemType);

        int health = 0;
        int armor = 0;
        int damage = 0;
        int attackDelay = 0;
        
        int statCount = GetStatCountByRarity(itemRarity);
        GetRandomStats(statCount, ref health, ref armor, ref damage, ref attackDelay, itemType);
        Item newItem = new Item(health, armor, damage, attackDelay, itemIcon, itemType, itemRarity);
        
        return newItem;
    }
    
    private void GetRandomStats(int statCount, ref int health, ref int armor, 
        ref int damage, ref int attackDelay, ItemType type)
    {
        health = 0;
        armor = 0;
        damage = 0;
        attackDelay = 0;
        
        StatType primaryStat = GetPrimaryStat(type);
        
        List<StatType> availableStats = new List<StatType>
        {
            StatType.Health,
            StatType.Armor,
            StatType.Damage,
            StatType.AttackDelay
        };
        
        AssignRandomValueToStat(primaryStat, ref health, ref armor, ref damage, ref attackDelay);
        availableStats.Remove(primaryStat);
        
        for (int i = 1; i < statCount; i++)
        {
            if (availableStats.Count == 0) break;

            int randomIndex = Random.Range(0, availableStats.Count);
            StatType chosenStat = availableStats[randomIndex];

            AssignRandomValueToStat(chosenStat, ref health, ref armor, ref damage, ref attackDelay);
            availableStats.RemoveAt(randomIndex);
        }
    }
    
    private void AssignRandomValueToStat(StatType statType, ref int health, ref int armor, 
        ref int damage, ref int attackDelay)
    {
        int min = 0, max = 0;

        switch (statType)
        {
            case StatType.Health:
                min = _minHealth;
                max = _maxHealth;
                health = Mathf.RoundToInt(Random.Range(min, max + 1));
                break;
            
            case StatType.Armor:
                min = _minArmor;
                max = _maxArmor;
                armor = Mathf.RoundToInt(Random.Range(min, max + 1));
                break;
            
            case StatType.Damage:
                min = _minDamage;
                max = _maxDamage;
                damage = Mathf.RoundToInt(Random.Range(min, max + 1));
                break;
            
            case StatType.AttackDelay:
                min = _minAttackDelay;
                max = _maxAttackDelay;
                attackDelay = Mathf.RoundToInt(Random.Range(min, max + 1));
                break;
        }
    }

    private StatType GetPrimaryStat(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Helmet:
                return StatType.Armor;
            case ItemType.Shield:
                return StatType.Armor;
            case ItemType.Sword:
                return StatType.Damage;
            case ItemType.Boots:
                return StatType.AttackDelay;
            case ItemType.Armor:
                return StatType.Health;

            case ItemType.Ring:
                StatType[] allStats = { StatType.Health, StatType.Armor, StatType.Damage, StatType.AttackDelay };
                return allStats[Random.Range(0, allStats.Length)];
            case ItemType.Necklace:
                StatType[] Stats = { StatType.Health, StatType.Armor, StatType.Damage, StatType.AttackDelay };
                return Stats[Random.Range(0, Stats.Length)];
            
            default:
                return StatType.Health;
        }
    }

    private ItemRarity GetRandomItemRarity()
    {
        float randomValue = Random.Range(0f, 100f);

        if (randomValue < 10f) return ItemRarity.Epic;
        if (randomValue < 20f) return ItemRarity.Rare;
        if (randomValue < 40f) return ItemRarity.Uncommon;
        
        return ItemRarity.Common;
    }

    private int GetStatCountByRarity(ItemRarity rarity)
    {
        switch (rarity)
        {
            case ItemRarity.Common: return 1;
            case ItemRarity.Uncommon: return 2;
            case ItemRarity.Rare: return 3;
            case ItemRarity.Epic: return 4;
            default: return 1;
        }
    }

    private float GetRarityMultiplier(ItemRarity rarity)
    {
        switch (rarity)
        {
            case ItemRarity.Common: return 1f;
            case ItemRarity.Uncommon: return 1.2f;
            case ItemRarity.Rare: return 1.5f;
            case ItemRarity.Epic: return 2f;
            default: return 0.5f;
        }
    }

    private Sprite GetIconForItemByType(ItemType itemType)
    {
        foreach (ItemInfo itemInfo in _itemData.Items)
        {
            if (itemInfo.ItemType == itemType) return itemInfo.ItemIcon;
        }
        
        return null;
    }
}
