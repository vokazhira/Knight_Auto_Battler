using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ItemStatsPanel _itemStatPanel;
    [SerializeField] private Image _itemImage;

    private Item _item;
    private bool _isEmpty = true;
    
    public bool IsEmpty => _isEmpty;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isEmpty)
        {
            _itemStatPanel.UpdateItem(_item);
            _itemStatPanel.UpdatePanelInfo();
            _itemStatPanel.ShowAboveSlot(this.GetComponent<RectTransform>());
            
            _itemStatPanel.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _itemStatPanel.gameObject.SetActive(false);
    }

    public void SetItem(Item item)
    {
        _item = item;
        _isEmpty = false;
        
        _itemImage.sprite = item.Sprite;
        _itemImage.enabled = true;
    }

    public void ClearSlot()
    {
        _item = null;
        _isEmpty = true;
        
        _itemImage.sprite = null;
        _itemImage.enabled = false;
    }
}
