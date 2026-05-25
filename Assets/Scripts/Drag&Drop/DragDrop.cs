using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item CurrentItem { get; private set; }
    
    private InventorySlot _currentSlot;
    private EquipmentSlot _equipmentSlot;

    private Transform _originalParent;
    private CanvasGroup _canvasGroup;
    private Canvas _canvas;

    private void Awake()
    { 
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvas = GetComponentInParent<Canvas>();
    }

    public void SetItem(Item item)
    {
        CurrentItem = item;
    }

    public void SetSlot(InventorySlot slot)
    {
        _currentSlot = slot;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _originalParent = transform.parent;
        transform.SetParent(transform.root);
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;

        var dropSlot = eventData.pointerEnter?.GetComponentInParent<InventorySlot>();

        if (dropSlot != null && dropSlot != _currentSlot && _currentSlot != null)
        {
            EventManager.Invoke(GameEvents.ItemMoved, new ItemMoveData(_currentSlot, dropSlot));
        }
        else
        {
            transform.SetParent(_originalParent);
            transform.localPosition = Vector3.zero;
        }
    }
}