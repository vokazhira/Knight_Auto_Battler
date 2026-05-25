using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventorySlot[] _inventorySlots;
    
    private void OnEnable()
    {
        EventManager.Subscribe(GameEvents.ItemMoved, OnItemMoved);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(GameEvents.ItemMoved, OnItemMoved);
    }

    public void AddItem(Item newItem)
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            if (_inventorySlots[i].IsEmpty)
            {
                _inventorySlots[i].SetItem(newItem);
                return;
            }
        }
        // TODO: Инвентарь полон
        Debug.LogWarning("Инвентарь полон!");
    }

    private void OnItemMoved(object obj)
    {
        if (obj is ItemMoveData moveData)
        {
            MoveItem(moveData.FromSlot, moveData.ToSlot);
        }
    }

    private void MoveItem(InventorySlot fromSlot, InventorySlot toSlot)
    {
        if (fromSlot == null || toSlot == null) return;

        Item itemToMove = fromSlot.CurrentItem;

        if (toSlot.IsEmpty)
        {
            fromSlot.ClearSlot();
            toSlot.SetItem(itemToMove);
        }
        else
        {
            Item temp = toSlot.CurrentItem;
            toSlot.ClearSlot();
            toSlot.SetItem(itemToMove);

            fromSlot.ClearSlot();
            fromSlot.SetItem(temp);
        }
    }

    public void RemoveItem() {}
}