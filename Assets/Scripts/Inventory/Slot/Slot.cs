using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected ItemStatsPanel _itemStatPanel;

    protected Item _currentItem;
    protected GameObject _currentSlotVisual;

    public Item CurrentItem => _currentItem;
    public bool IsEmpty => _currentItem == null;

    public virtual void SetItem(Item item)
    {
        if (item == null) return;

        _currentItem = item;
    }

    public virtual void ClearSlot()
    {
        _currentItem = null;
    }

    public virtual bool CanAcceptItem(Item item)
    {
        return item != null;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (IsEmpty || _currentItem == null || _itemStatPanel == null)
        {
            if (_itemStatPanel != null && _itemStatPanel.gameObject.activeSelf)
            {
                _itemStatPanel.gameObject.SetActive(false);
            }
            
            return;
        }
        
        _itemStatPanel.UpdateItem(_currentItem);
        _itemStatPanel.UpdatePanelInfo();
        _itemStatPanel.ShowAboveSlot(GetComponent<RectTransform>());
        _itemStatPanel.gameObject.SetActive(true);
        
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (_itemStatPanel != null)
        {
            _itemStatPanel.gameObject.SetActive(false);
        }
    }
}