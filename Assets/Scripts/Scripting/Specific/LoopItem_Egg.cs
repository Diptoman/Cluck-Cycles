using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopItem_Egg : LoopItem
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
        }
    }
}
