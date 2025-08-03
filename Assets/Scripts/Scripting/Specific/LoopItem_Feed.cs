using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopItem_Feed : LoopItem
{
    public override void Process()
    {
        base.Process();

        switch (action)
        {
            case Actions.Grow:
                InventoryController.SetItemCount(ItemType.Crops, InventoryController.GetItemCount(ItemType.Crops) + 1);
                FunctionContainerRef.DoNotReset();
                break;
        }
    }
}
