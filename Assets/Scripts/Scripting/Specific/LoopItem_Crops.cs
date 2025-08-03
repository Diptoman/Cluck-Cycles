using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopItem_Crops : LoopItem
{
    public override void Process()
    {
        base.Process();

        switch (action)
        {
            case Actions.Harvest:
                InventoryController.SetItemCount(ItemType.Feed, InventoryController.GetItemCount(ItemType.Feed) + 2);
                FunctionContainerRef.DoNotReset();
                break;
        }
    }
}
