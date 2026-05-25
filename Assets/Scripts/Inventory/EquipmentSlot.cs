using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] private ItemType _acceptedType;
    
    public Item CurrentItem { get; private set; }
    public ItemType AcceptedType => _acceptedType;
    
    private Image _iconImage;

    private void Awake()
    {
        
    }
    
    public bool CanEquip(Item item)
    {
        return item != null && item.Type == _acceptedType;
    }
    
    public bool IsCorrectTypeForSlot(ItemType itemType)
    {
        //Пока заглушка
        return true;
    }
    
    public void EquipItem(Item item)
    {
        CurrentItem = item;
        // TODO: Обновить визуал слота (изображение)
    }

    public void UnequipItem()
    {
        CurrentItem = null;
        // TODO: Очистить визуал
    }
}
