using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ItemData", fileName = "ItemData", order = 52)]
public class ItemData : ScriptableObject
{
    public ItemInfo[] Items;
}

[Serializable]
public struct ItemInfo
{
    public ItemType ItemType;
    public Sprite ItemIcon;
}

public enum ItemType
{
    Helmet,
    Shield,
    Sword,
    Armor,
    Boots,
    Ring,
    Necklace
}

public enum ItemRarity
{
    Common = 0,
    Uncommon = 1,
    Rare = 2,
    Epic = 3
}

