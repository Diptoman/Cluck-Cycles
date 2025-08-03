using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopItem_Rooster : LoopItem
{
    public AudioClip CluckClip;
    public AudioClip FertilizeClip;
    public AudioClip CookClip;
    public override void Process()
    {
        base.Process();

        //Additional stuff
        switch (action)
        {
            case Actions.Cluck:
                CluckController.ClucksRemaining += 2;
                MusicController.Instance.PlaySFX(CluckClip);
                break;

            case Actions.Fertilize:
                InventoryController.SetItemCount(ItemType.ChickenSperm, InventoryController.GetItemCount(ItemType.ChickenSperm) + 1);
                MusicController.Instance.PlaySFX(FertilizeClip);
                break;

            case Actions.Cook:
                InventoryController.SetItemCount(ItemType.FriedChicken, InventoryController.GetItemCount(ItemType.FriedChicken) + 1);
                MusicController.Instance.PlaySFX(CookClip);
                FunctionContainerRef.DoNotReset();
                break;
        }
    }
}
