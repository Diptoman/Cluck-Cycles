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
            shopSlots[i].Setup(ItemType.Invalid, 0);
        }
    }

    public static void Destroy()
    {
        Instance = null;
    }

    public static int GetPrice(int index)
    {
        return Instance.shopSlots[index].price;
    }

    public static void SetItem(int index, ItemType type)
    {
        var price = Global.GetItemInfo(type).buyPrice;
        Instance.shopSlots[index].Setup(type, price);
    }
}