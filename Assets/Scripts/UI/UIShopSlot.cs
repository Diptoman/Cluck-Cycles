using UnityEngine;
using TMPro;

public class UIShopSlot : MonoBehaviour
{
    public bool isForLoop;
    public ItemType itemType;
    public Sprite sprite;
    public SpriteRenderer slotSprite;
    public FloatAnimation floatAnim;
    public FloatAnimation floatAnimText;
    public TextMeshPro priceText;
    public Color activeTextColor;
    public int buyPrice { get; private set; }

    public void Setup(ItemType type)
    {
        var info = Global.GetItemInfo(type);
        this.buyPrice = info.buyPrice;
        this.itemType = type;
        priceText.SetText("$" + buyPrice);
        slotSprite.sprite = info.sprite;
    }

    public void Reroll(ItemType type)
    {
        var info = Global.GetItemInfo(type);
        this.buyPrice = info.GetBuyPriceRandom();
        this.itemType = type;
        priceText.SetText("$" + buyPrice);
        slotSprite.sprite = info.sprite;
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

        if (buyPrice <= Global.Money && itemType != ItemType.Invalid)
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
        if (Global.Money >= buyPrice)
        {
            Global.Money -= buyPrice;
            var current = InventoryController.GetItemCount(itemType);
            InventoryController.SetItemCount(itemType, current + 1);
            ReRollItem();
        }
        else
            Debug.Log($"Buy failed");
    }

    void ReRollItem()
    {
        if (isForLoop)
            return;

        var num = Global.availableInShop.Length;
        var next = UnityEngine.Random.Range(0, num);
        var newType = Global.availableInShop[next];
        Reroll(newType);
    }
}
