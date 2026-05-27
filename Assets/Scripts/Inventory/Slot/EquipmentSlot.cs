using Lean.Pool;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : Slot
{
    [SerializeField] private GameObject _slotItemPrefab;

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

        if (_currentSlotVisual == null && _slotItemPrefab != null)
        {
            _currentSlotVisual = LeanPool.Spawn(_slotItemPrefab, transform);
            
            RectTransform rect = _currentSlotVisual.GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            rect.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }

        Image itemImage = _currentSlotVisual?.GetComponent<Image>();
        if (itemImage != null)
        {
            itemImage.sprite = item.Sprite;
            Color color = itemImage.color;
            color.a = 1f;
            itemImage.color = color;
        }

        DragDrop dragDrop = _currentSlotVisual?.GetComponent<DragDrop>();
        if (dragDrop != null)
        {
            dragDrop.SetItem(_currentItem);
            dragDrop.SetSlot(this);
        }
    }

    public override void ClearSlot()
    {
        base.ClearSlot();

        if (_currentSlotVisual != null)
        {
            LeanPool.Despawn(_currentSlotVisual);
            _currentSlotVisual = null;
        }
    }
}