using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopItem_Chicken : LoopItem
{
    public AudioClip CluckClip;
    public AudioClip LayEggClip;
    public AudioClip WowClip;
    public AudioClip CookClip;
    public override void Process()
    {
        base.Process();

        //Additional stuff
        switch (action)
        {
            case Actions.Cluck:
                CluckController.ClucksRemaining += 1;
                MusicController.Instance.PlaySFX(CluckClip);
                break;

            case Actions.LayEggs:
                if (InventoryController.GetItemCount(ItemType.ChickenSperm) > 0)
                {
                    if (Random.Range(0, 15) == 0)
                    {
                        InventoryController.SetItemCount(ItemType.GoldenEgg, InventoryController.GetItemCount(ItemType.GoldenEgg) + 1);
                        MusicController.Instance.PlaySFX(WowClip, true);
                    }
                    else
                    {
                        InventoryController.SetItemCount(ItemType.FertileEgg, InventoryController.GetItemCount(ItemType.FertileEgg) + 1);
                        MusicController.Instance.PlaySFX(LayEggClip);
                    }

                    InventoryController.SetItemCount(ItemType.ChickenSperm, InventoryController.GetItemCount(ItemType.ChickenSperm) - 1);
                }
                else
                {
                    if (Random.Range(0, 10) == 0)
                    {
                        InventoryController.SetItemCount(ItemType.GoldenEgg, InventoryController.GetItemCount(ItemType.GoldenEgg) + 1);
                        MusicController.Instance.PlaySFX(WowClip, true);
                    }
                    else
                    {
                        InventoryController.SetItemCount(ItemType.Egg, InventoryController.GetItemCount(ItemType.Egg) + 1);
                        MusicController.Instance.PlaySFX(LayEggClip);
                    }
                }
                break;

            case Actions.Cook:
                InventoryController.SetItemCount(ItemType.FriedChicken, InventoryController.GetItemCount(ItemType.FriedChicken) + 1);
                FunctionContainerRef.DoNotReset();
                MusicController.Instance.PlaySFX(CookClip);
                break;
        }
    }
}
