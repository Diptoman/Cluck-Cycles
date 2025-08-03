
public static class Consts
{
    public const float TOOLTIP_TIME = 0.5f;

    public const string SELL_EVENT = "SellEvent";
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
    MAX = 7 //Move this down and increase it as you add more
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