using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] private EquipmentSlot[] _slots;

    private void OnEnable()
    {
        EventManager.Subscribe(GameEvents.ItemEquipped, OnItemEquipped);
        EventManager.Subscribe(GameEvents.ItemUnequipped, OnItemUnequipped);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(GameEvents.ItemEquipped, OnItemEquipped);
        EventManager.Unsubscribe(GameEvents.ItemUnequipped, OnItemUnequipped);
    }

    private void OnItemEquipped(object obj)
    {
        
    }

    private void OnItemUnequipped(object obj)
    {
        
    }
}
