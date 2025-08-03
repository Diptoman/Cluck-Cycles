using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopItem_Chick : LoopItem
{
    public AudioClip CookClip;
    public AudioClip FeedClip;

    public override void Process()
    {
        base.Process();

        //Additional stuff
        switch (action)
        {
            case Actions.Cook:
                FunctionContainerRef.DoNotReset();
                MusicController.Instance.PlaySFX(CookClip);
                break;

            case Actions.Feed:
                if (InventoryController.GetItemCount(ItemType.Feed) > 0)
                {
                    if (Random.value < 0.5f)
                    {
                        InventoryController.SetItemCount(ItemType.Chicken, InventoryController.GetItemCount(ItemType.Chicken) + 1);
                    }
                    else
                    {
                        InventoryController.SetItemCount(ItemType.Rooster, InventoryController.GetItemCount(ItemType.Rooster) + 1);
                    }
                    MusicController.Instance.PlaySFX(FeedClip);
                    InventoryController.SetItemCount(ItemType.Feed, InventoryController.GetItemCount(ItemType.Feed) - 1);
                }
                FunctionContainerRef.DoNotReset();
                break;
        }
    }
}
