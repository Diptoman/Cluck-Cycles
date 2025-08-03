using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopItem_Fertile : LoopItem
{
    public AudioClip CookClip;
    public AudioClip IncubateClip;
    public AudioClip WowClip;
    public override void Process()
    {
        base.Process();

        switch (action)
        {
            case Actions.Cook:
                InventoryController.SetItemCount(ItemType.Omelette, InventoryController.GetItemCount(ItemType.Omelette) + 1);
                FunctionContainerRef.DoNotReset();
                MusicController.Instance.PlaySFX(CookClip);
                break;

            case Actions.Incubate:
                if (Random.Range(0, 15) == 0)
                {
                    InventoryController.SetItemCount(ItemType.GoldenGoose, InventoryController.GetItemCount(ItemType.GoldenGoose) + 1);
                    MusicController.Instance.PlaySFX(WowClip, true);
                }
                else
                {
                    InventoryController.SetItemCount(ItemType.Chick, InventoryController.GetItemCount(ItemType.Chick) + 1);
                    MusicController.Instance.PlaySFX(IncubateClip);
                }
                FunctionContainerRef.DoNotReset();
                break;
        }
    }
}
