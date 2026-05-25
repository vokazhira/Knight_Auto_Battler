using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMoveData
{
    public InventorySlot FromSlot { get; }
    public InventorySlot ToSlot { get; }

    public ItemMoveData(InventorySlot from, InventorySlot to)
    {
        FromSlot = from;
        ToSlot = to;
    }
}
