using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentBonusData
{
    public Item Item { get; }
    public bool IsEquip { get; }

    public EquipmentBonusData(Item item, bool isEquip)
    {
        Item = item;
        IsEquip = isEquip;
    }
}
