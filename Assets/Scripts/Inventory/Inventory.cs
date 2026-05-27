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
        Debug.LogWarning("Инвентарь полон!");
    }

    private void MoveItem(Slot fromSlot, Slot toSlot)
    {
        if (fromSlot == null || toSlot == null || fromSlot == toSlot) return;
        
        Item itemToMove = fromSlot.CurrentItem;
        if (itemToMove == null) return;
        
        if (!toSlot.CanAcceptItem(itemToMove)) return;
        
        Item itemInTarget = toSlot.CurrentItem;
        
        if (itemInTarget != null && toSlot is EquipmentSlot)
        {
            EventManager.Invoke(GameEvents.ItemUnequipped, itemInTarget);
        }
        
        if (fromSlot is EquipmentSlot)
        {
            EventManager.Invoke(GameEvents.ItemUnequipped, itemToMove);
        }
        
        fromSlot.ClearSlot();
        toSlot.ClearSlot();
    
        if (itemInTarget != null)
        {
            fromSlot.SetItem(itemInTarget);
            if (fromSlot is EquipmentSlot)
            {
                EventManager.Invoke(GameEvents.ItemEquipped, itemInTarget);
            }
        }
    
        toSlot.SetItem(itemToMove);
        if (toSlot is EquipmentSlot)
        {
            EventManager.Invoke(GameEvents.ItemEquipped, itemToMove);
        }
    }
    
    private void OnItemMoved(object obj)
    {
        if (obj is ItemMoveData moveData)
        {
            MoveItem(moveData.FromSlot, moveData.ToSlot);
        }
    }

    public void RemoveItem() {}
}