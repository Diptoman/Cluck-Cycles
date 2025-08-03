using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class UIShopSlot : MonoBehaviour
{
    public ItemType itemType;
    public Sprite sprite;
    public SpriteRenderer slotSprite;
    public FloatAnimation floatAnim;
    public FloatAnimation floatAnimText;
    public TextMeshPro priceText;
    public Color activeTextColor;
    public int buyPrice;

    [Header("ForLoop")]
    public bool isForLoop;
    public int loopNum = 2;
    public int loopSlots = 1;

    public TextMeshPro loopNumText;
    public TextMeshPro loopSlotsText;

    [Button("Setup")]
    public void Setup()
    {
        if (isForLoop)
        {
            loopNumText.SetText(loopNum.ToString());
            loopSlotsText.SetText($"({loopSlots})");
            priceText.SetText("$" + buyPrice);
            return;
        }

        var info = Global.GetItemInfo(itemType);
        slotSprite.sprite = info.sprite;
        buyPrice = info.GetBuyPriceRandom();
        priceText.SetText("$" + buyPrice);
    }

    public void Reroll()
    {
        if (isForLoop)
        {
            loopNum = Random.Range(Global.LoopNumMin, Global.LoopSlotsMax + 1);
            loopSlots = Random.Range(Global.LoopSlotsMin, Global.LoopSlotsMax + 1);
            buyPrice = Global.GetRandomLoopPrice(loopNum, loopSlots);

            loopNumText.SetText(loopNum.ToString());
            loopSlotsText.SetText($"({loopSlots})");
            priceText.SetText("$" + buyPrice);
            return;
        }

        var num = Global.availableInShop.Length;
        var next = UnityEngine.Random.Range(0, num);
        var newType = Global.availableInShop[next];

        var info = Global.GetItemInfo(newType);
        this.buyPrice = info.GetBuyPriceRandom();
        this.itemType = newType;
        priceText.SetText("$" + buyPrice);
        slotSprite.sprite = info.sprite;
    }

    public void Update()
    {
        var isValid = isForLoop || itemType != ItemType.Invalid;
        if (!isValid)
        {
            this.gameObject.SetActive(false);
            return;
        }
        else
        {
            this.gameObject.SetActive(true);
        }

        if (buyPrice <= Global.Money && isValid)
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
        if (CluckController.IsProcessing)
            return;

        if (Global.Money >= buyPrice)
        {
            Global.Money -= buyPrice;

            if (isForLoop)
            {
                SlotController.Instance.AddForLoop(loopSlots, loopNum);
            }
            else
            {
                var current = InventoryController.GetItemCount(itemType);
                InventoryController.SetItemCount(itemType, current + 1);
            }
            Reroll();
        }
        else
            Debug.Log($"Buy failed");
    }
}
