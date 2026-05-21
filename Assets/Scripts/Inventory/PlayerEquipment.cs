using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] private EquipmentSlot[] _slots;

    public void EquipItem()
    {
        //TODO: Передать значение айтема в PlayerEntity + PlayerStats. Через событие?
    }

    public void UnequipItem()
    {
        //TODO: вернуть предмет в инвентарь 
    }
}
