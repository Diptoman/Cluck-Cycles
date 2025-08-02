using System.Collections.Generic;
using UnityEngine;

public class InventoryController
{
    public static InventoryController Instance { get; private set; }
    private Dictionary<SlotObject, DraggableObject_Item> slotToItemMap;

    public static void Init(UIController controller)
    {
        Instance = new InventoryController(controller);
    }

    public InventoryController(UIController controller)
    {
        var num = controller.itemSlots.Length;
        // slotToItemMap = new(num);
        // for (int i = 0; i < num; i++)
        // {
        //     slotToItemMap.TryAdd(controller.itemSlots[i].slotObject, null);
        // }
    }

    public static void Destroy()
    {
        Instance = null;
    }

    public static void CheckAndHighlightSlot(SlotObject slotObj, DraggableObject_Item item)
    {
        var currentItem = Instance.slotToItemMap[slotObj];

        if (currentItem != null && currentItem != item)
        {
            item.currentSlot = slotObj;
            slotObj.SetState(SlotState.Reserved);
        }
        else
        {
            item.currentSlot = slotObj;
            slotObj.SetState(SlotState.Highlighted);
        }

        Debug.Log($"Highlight slot");
    }

    public static void CheckAndUnhighlightSlot(SlotObject slotObj, DraggableObject_Item item)
    {
        var currentItem = Instance.slotToItemMap[slotObj];
        if (currentItem == null)
            slotObj.SetState(SlotState.Unoccupied);
        else
            slotObj.SetState(SlotState.Occupied);

        Debug.Log($"Unhighlight slot");
    }
    public static void CheckAndAssignSlot(SlotObject slotObj, DraggableObject_Item item)
    {
        var currentItem = Instance.slotToItemMap[slotObj];
        if (currentItem != null)
        {
            item.currentSlot = item.lastAssignedSlot;
            Instance.slotToItemMap[item.lastAssignedSlot] = item;

            Debug.Log($"Assign slot failed");
            return;
        }

        Instance.slotToItemMap[slotObj] = item;
        slotObj.SetState(SlotState.Occupied);
        item.currentSlot = slotObj;
        item.Parent.transform.SetParent(slotObj.transform);
        var pos = slotObj.transform.position;
        pos.z = item.Parent.transform.position.z;
        item.Parent.transform.position = pos;
        item.initialPosition = pos;
        item.lastAssignedSlot = slotObj;


        Debug.Log($"Assign slot");
    }

    public static void CheckAndUnAssignSlot(SlotObject slotObject, DraggableObject_Item item)
    {
        Instance.slotToItemMap[slotObject] = null;
        item.currentSlot = null;
        Debug.Log($"Unassign slot");
    }
}