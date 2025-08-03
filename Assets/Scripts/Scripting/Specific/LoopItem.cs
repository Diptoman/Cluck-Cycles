using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoopItem : MonoBehaviour
{
    public FunctionContainer FunctionContainerRef;
    public AudioClip sellClip;
    protected ItemType itemType;
    protected Actions action;

    public virtual void Process()
    {
        itemType = FunctionContainerRef.GetSelectedItemType();
        action = FunctionContainerRef.GetSelectedAction();
        CluckController.Process(itemType, action);

        if (action == Actions.Sell)
        {
            Debug.Log("Selling index " + (int)itemType + " " + itemType);
            EventController.TriggerEvent(Consts.SELL_EVENT, (int)itemType);
            FunctionContainerRef.DoNotReset();
            MusicController.Instance.PlaySFX(sellClip);
        }
    }
}
