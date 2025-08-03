using UnityEngine;
using TMPro;

public class UIShopSlot : MonoBehaviour
{
    public ItemType itemType;
    public Sprite sprite;
    public SpriteRenderer slotSprite;
    public FloatAnimation floatAnim;
    public FloatAnimation floatAnimText;
    public TextMeshPro priceText;
    public Color activeTextColor;
    public int price { get; private set; }

    public void Setup(ItemType type, int price)
    {
        this.price = price;
        this.itemType = type;
        priceText.SetText("$" + price);
        slotSprite.sprite = Global.GetItemInfo(type).sprite;
    }

    public void Update()
    {
        if (itemType == ItemType.Invalid)
        {
            this.gameObject.SetActive(false);
            return;
        }
        else
        {
            this.gameObject.SetActive(true);
        }

        if (price <= Global.Money && itemType != ItemType.Invalid)
        {
            floatAnim?.SetActive(true);
            floatAnimText?.SetActive(true);
            slotSprite.color = Color.white;
            priceText.color = activeTextColor;
        }
        else
        {
            floatAnim?.SetActive(false);
            floatAnimText?.SetActive(false);
            slotSprite.color = Color.gray;
            priceText.color = Color.gray;
        }
    }

    void OnMouseDown()
    {
        Debug.Log($"TRY BUY");
        if (Global.Money >= price)
        {
            Global.Money -= price;
            var current = InventoryController.GetItemCount(itemType);
            InventoryController.SetItemCount(itemType, current + 1);
            RandomizeItem();
        }
        else
            Debug.Log($"Buy failed");
    }

    void RandomizeItem()
    {

    }
}
