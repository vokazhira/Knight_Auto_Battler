using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemStatsPanel : MonoBehaviour
{
    [SerializeField] private Image _rarityBorderImage;
    
    [SerializeField] private ItemStatLinePanel _healthItemStatLine;
    [SerializeField] private ItemStatLinePanel _armorItemStatLine;
    [SerializeField] private ItemStatLinePanel _damageItemStatLine;
    [SerializeField] private ItemStatLinePanel _attackDelayItemStatLine;
    
    [SerializeField] private TextMeshProUGUI _salePriceText;
    
    [SerializeField] private Canvas _canvas;
    
    private Item _item; 
    
    public void UpdateItem(Item item)
    {
        _item = item;
    }

    public void UpdatePanelInfo()
    {
        if (_item == null)
        {
            gameObject.SetActive(false);
            return;
        }
        
        UpdateRarityBorderColor();
        UpdateAllItemStatLines();
        UpdateSalePrice();
    }

    public void ShowAboveSlot(RectTransform slotTransform)
    {
        if (slotTransform == null) return;
        
        RectTransform panelRect = GetComponent<RectTransform>();
        
        Vector3 slotWorldPos = slotTransform.position;

        slotWorldPos.y += 150f;
        
        panelRect.position = slotWorldPos;
    }

    private void UpdateAllItemStatLines()
    {
        UpdateItemStatLine(_healthItemStatLine, _item.Health);
        UpdateItemStatLine(_armorItemStatLine, _item.Armor);
        UpdateItemStatLine(_damageItemStatLine, _item.Damage);
        UpdateItemStatLine(_attackDelayItemStatLine, _item.AttackDelay);
    }

    private void UpdateRarityBorderColor()
    {
        if (_item == null) return;

        switch (_item.Rarity)
        {
            case ItemRarity.Common:
                _rarityBorderImage.color = Color.gray;
                break;
            case ItemRarity.Uncommon:
                _rarityBorderImage.color = Color.green;
                break;
            case ItemRarity.Rare:
                _rarityBorderImage.color = Color.blue;
                break;
            case ItemRarity.Epic:
                _rarityBorderImage.color = Color.magenta;
                break;
        }
    }

    private void UpdateItemStatLine(ItemStatLinePanel itemStatLine, int value)
    {
        bool hasStat = value != 0;
        itemStatLine.gameObject.SetActive(hasStat);

        if (hasStat)
        {
            itemStatLine.SetStatValueText(value);
        }
    }
    
    private void UpdateSalePrice()
    {
        if (_salePriceText == null) return;

        if (_item != null)
        {
            _salePriceText.text = $"{_item.SalePrice}";
            _salePriceText.gameObject.SetActive(true);
        }
        else
        {
            _salePriceText.gameObject.SetActive(false);
        }
    }
}
