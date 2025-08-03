using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopItem_Rooster : LoopItem
{
    public override void Process()
    {
        base.Process();

        //Additional stuff
        switch (action)
        {
            case Actions.Cluck:
                CluckController.ClucksRemaining += 2;
                break;

            case Actions.Fertilize:
                InventoryController.SetItemCount(ItemType.ChickenSperm, InventoryController.GetItemCount(ItemType.ChickenSperm) + 1);
                break;

            case Actions.Cook:
                InventoryController.SetItemCount(ItemType.FriedChicken, InventoryController.GetItemCount(ItemType.FriedChicken) + 1);
                FunctionContainerRef.DoNotReset();
                break;
        }
    }
}
