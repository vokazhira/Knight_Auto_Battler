using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using UnityEngine.EventSystems;

public class SellZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        
        DragDrop dragDrop = eventData.pointerDrag.GetComponent<DragDrop>();
        if (dragDrop == null || dragDrop.CurrentItem == null) return;
        
        Item item = dragDrop.CurrentItem;
        Slot slot = dragDrop.GetSlot();
        
        if (slot is EquipmentSlot)
        {
            EventManager.Invoke(GameEvents.ItemUnequipped, item);
        }
        
        CurrencyManager.Instance.AddCurrency(item.SalePrice);

        if (slot != null)
        {
            slot.ClearSlot();
        }
        
        LeanPool.Despawn(dragDrop.gameObject);
    }
}
