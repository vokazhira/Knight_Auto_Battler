using UnityEngine;

public class EquipmentSlot : Slot
{
    [SerializeField] private ItemType _acceptedType;

    public ItemType AcceptedType => _acceptedType;

    public override bool CanAcceptItem(Item item)
    {
        return item != null && item.Type == _acceptedType;
    }

    public override void SetItem(Item item)
    {
        if (!CanAcceptItem(item)) return;

        base.SetItem(item);
        // TODO: Обновить визуальное отображение предмета в слоте экипировки
    }

    public override void ClearSlot()
    {
        base.ClearSlot();
        // TODO: Очистить визуальное отображение
    }
}