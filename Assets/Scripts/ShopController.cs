using UnityEngine;


public class ShopController
{
    public static ShopController Instance { get; private set; }

    public static void Init(UIController controller)
    {
        Instance = new ShopController(controller);
    }

    private UIController uiController;
    private UIShopSlot[] shopSlots => uiController.shopSlots;


    public ShopController(UIController controller)
    {
        uiController = controller;

        for (var i = 0; i < shopSlots.Length; i++)
        {
            // shopSlots[i].Setup(ItemType.Invalid);
            shopSlots[i].Reroll();
        }

        // shopSlots[3].Reroll();
        // shopSlots[4].Reroll();
        // shopSlots[5].Reroll();
        // ShopController.SetItem(1, ItemType.Chicken);
    }

    public static void Destroy()
    {
        Instance = null;
    }

    public static int GetPrice(int index)
    {
        return Instance.shopSlots[index].buyPrice;
    }

    public static void SetItem(int index, ItemType type)
    {
        Instance.shopSlots[index].Setup(type);
    }
}