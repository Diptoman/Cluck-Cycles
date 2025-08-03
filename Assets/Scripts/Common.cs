using System;
using UnityEngine;

public static class Consts
{
    public const float TOOLTIP_TIME = 0.5f;

    public const string SELL_EVENT = "SellEvent";
    public const string CPURESET_EVENT = "CPUReset";
}

public static class Global
{
    //Shop loop randomization values, can be increased as game progresses
    public static int LoopSlotsMin = 1;
    public static int LoopSlotsMax = 2;
    public static int LoopNumMin = 2;
    public static int loopNumMax = 4;

    public static int GetRandomLoopPrice(int loopNum, int loopSlots)
    {
        return loopNum * 4 + loopSlots * 10 + UnityEngine.Random.Range(0, 5);
    }

    public static readonly ItemType[] availableInShop =
    {
        ItemType.Chicken,
        ItemType.Rooster,
        ItemType.Feed,
        ItemType.Egg
     };

    public static int Money = 5;
    public static int CPUPrice = 20;

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

    public static ItemInfo GetItemInfo(ItemInfo[] itemSprites, ItemType type)
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
        switch (type)
        {
            case Consts.SELL_EVENT:
                Debug.Log($"Sell event");
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
    public int buyPriceMax;
    public bool isTransient;
    public GameObject draggableItem;

    public int GetBuyPriceRandom()
    {
        if (buyPriceMax <= buyPrice)
            return buyPrice;

        return UnityEngine.Random.Range(buyPrice, buyPriceMax + 1);
    }
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
    GoldenEgg = 10,
    GoldenGoose = 11,
    MAX = 12 //Move this down and increase it as you add more
}

public enum Actions
{
    Cluck = 0,
    LayEggs = 1,
    Sell = 2,
    Fertilize = 3,
    Incubate = 4,
    Cook = 5,
    Feed = 6,
    Grow = 7,
    Harvest = 8
}