using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopItem_Fertile : LoopItem
{
    public override void Process()
    {
        base.Process();

        switch (action)
        {
            case Actions.Cook:
                InventoryController.SetItemCount(ItemType.Omelette, InventoryController.GetItemCount(ItemType.Omelette) + 1);
                FunctionContainerRef.DoNotReset();
                break;

            case Actions.Incubate:
                InventoryController.SetItemCount(ItemType.Chick, InventoryController.GetItemCount(ItemType.Chick) + 1);
                FunctionContainerRef.DoNotReset();
                break;
        }
    }
}
