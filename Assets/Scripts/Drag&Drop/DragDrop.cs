using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public Item CurrentItem { get; private set; }

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
        
        if (eventData.pointerEnter?.GetComponent<DropSlot>() != null)
        {
            transform.SetParent(eventData.pointerEnter.transform);
            transform.localPosition = Vector3.zero;
        }
        else
        {
            transform.SetParent(_originalParent);
            transform.localPosition = Vector3.zero;
            Debug.Log("Не вижу скрипта дроп слот");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}