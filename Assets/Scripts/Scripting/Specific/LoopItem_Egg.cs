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
            case Actions.Incubate:
                InventoryController.SetItemCount(ItemType.Chick, InventoryController.GetItemCount(ItemType.Chick) + 1);
                break;

            case Actions.Cook:
                InventoryController.SetItemCount(ItemType.Omelette, InventoryController.GetItemCount(ItemType.Omelette) + 1);
                break;
        }
    }
}
