using UnityEngine;


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