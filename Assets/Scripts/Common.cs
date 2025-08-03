using System;
using UnityEngine;

public static class Consts
{
    public const float TOOLTIP_TIME = 0.5f;

    public const string SELL_EVENT = "SellEvent";
}

public static class Global
{
    public static int Money = 5;
    public static ItemInfo[] itemSprites;

    public static ItemInfo GetItemInfo(ItemType type)
    {
        ItemInfo result = itemSprites[0];
        for (var i = 0; i < itemSprites.Length; i++)
        {
            var element = itemSprites[i];
            if (element.type == type)
                return element;
        }
        return result;
    }


    public static void ProcessEvent(string type, int intVal = 0, float floatVal = 0)
    {
        Debug.Log($"Sell event");
        switch (type)
        {
            case Consts.SELL_EVENT:
                Global.Money += GetItemInfo((ItemType)intVal).sellPrice;
                break;
        }
        return;
    }
}

[Serializable]
public struct ItemInfo
{
    public ItemType type;
    public Sprite sprite;
    public int sellPrice;
    public int buyPrice;
    public GameObject draggableItem;
}


public enum ItemType
{
    Invalid = -1,
    Chicken = 0,
    Egg = 1,
    Rooster = 2,
    Feed = 3,
    Crops = 4,
    FertileEgg = 5,
    Chick = 6,
    Omelette = 7,
    FriedChicken = 8,
    ChickenSperm = 9,
    MAX = 10 //Move this down and increase it as you add more
}

public enum Actions
{
    Cluck = 0,
    LayEggs = 1,
    Sell = 2,
    Fertilize = 3,
    Incubate = 4,
    Cook = 5
}