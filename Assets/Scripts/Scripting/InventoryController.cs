using UnityEngine;
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

public class InventoryController
{


    public static InventoryController Instance { get; private set; }
    private UIInventorySlot[] itemIdToSlotMap;

    public static void Init(UIController controller)
    {
        Instance = new InventoryController(controller);
    }

    public InventoryController(UIController controller)
    {
        var num = (int)ItemType.MAX;
        itemIdToSlotMap = new UIInventorySlot[(int)ItemType.MAX];

        for (int i = 0; i < num; i++)
        {
            itemIdToSlotMap[i] = controller.itemSlots[i];
            itemIdToSlotMap[i].itemType = (ItemType)i;
        }
    }

    public static void Destroy()
    {
        Instance = null;
    }

    public static int GetItemCount(ItemType type)
    {
        return Instance.itemIdToSlotMap[(int)type].stackSize;
    }

    public static void SetItemCount(ItemType type, int value)
    {
        Instance.itemIdToSlotMap[(int)type].SetStackSize(value);
    }
}