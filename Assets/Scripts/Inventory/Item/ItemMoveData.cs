using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMoveData
{
    public Slot FromSlot { get; }
    public Slot ToSlot { get; }

    public ItemMoveData(Slot from, Slot to)
    {
        FromSlot = from;
        ToSlot = to;
    }
}
