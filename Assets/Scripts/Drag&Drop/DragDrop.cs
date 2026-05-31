using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item CurrentItem { get; private set; }
    
    private Slot _currentSlot;

    private Transform _originalParent;
    private CanvasGroup _canvasGroup;

    private void Awake()
    { 
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetItem(Item item)
    {
        CurrentItem = item;
    }

    public void SetSlot(Slot slot)
    {
        _currentSlot = slot;
    }

    public Slot GetSlot()
    {
        return _currentSlot;
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
        
        Slot dropSlot = eventData.pointerEnter?.GetComponentInParent<Slot>();
 
        if (dropSlot != null && dropSlot != _currentSlot && _currentSlot != null)
        {
            if (dropSlot.CanAcceptItem(CurrentItem))
            {
                EventManager.Invoke(GameEvents.ItemMoved, new ItemMoveData(_currentSlot, dropSlot));
            }
            else
            {
                ReturnToOriginalPosition();
            }
        }
        else
        {
            ReturnToOriginalPosition();
        }
    }

    private void ReturnToOriginalPosition()
    {
        transform.SetParent(_originalParent);
        transform.localPosition = Vector3.zero;
    }
}