using Lean.Pool;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ItemStatsPanel _itemStatPanel;
    [SerializeField] private GameObject _slotItemPrefab;
    
    private Item _currentItem;
    private GameObject _currentSlotItem;
    private bool _isEmpty = true;
    
    public bool IsEmpty => _isEmpty;
    public Item CurrentItem => _currentItem;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isEmpty && _currentItem != null)
        {
            _itemStatPanel.UpdateItem(_currentItem);
            _itemStatPanel.UpdatePanelInfo();
            _itemStatPanel.ShowAboveSlot(GetComponent<RectTransform>());
            
            _itemStatPanel.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _itemStatPanel.gameObject.SetActive(false);
    }

    public void SetItem(Item item)
    {
        if (item == null) return;

        _currentItem = item;
        _isEmpty = false;

        if (_currentSlotItem == null)
        {
            _currentSlotItem = LeanPool.Spawn(_slotItemPrefab, transform);
            
            RectTransform rectTransform = _currentSlotItem.GetComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
        
        Image itemImage = _currentSlotItem.GetComponent<Image>();
        if (itemImage != null)
        {
            itemImage.sprite = item.Sprite;
            Color color = itemImage.color;
            color.a = 1f;
            itemImage.color = color;
        }
        
        DragDrop dragDrop = _currentSlotItem.GetComponent<DragDrop>();
        if (dragDrop != null)
        {
            dragDrop.SetItem(_currentItem);
            dragDrop.SetSlot(this);
        }
    }

    public void ClearSlot()
    {
        _currentItem = null;
        _isEmpty = true;
        
        if (_currentSlotItem != null)
        {
            LeanPool.Despawn(_currentSlotItem);
            _currentSlotItem = null;
        }
    }
}