using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoopItem : MonoBehaviour
{
    public FunctionContainer FunctionContainerRef;
    protected ItemType itemType;
    protected Actions action;

    public virtual void Process()
    {
        itemType = FunctionContainerRef.GetSelectedItemType();
        action = FunctionContainerRef.GetSelectedAction();
        CluckController.Process(itemType, action);

        if (action == Actions.Sell)
        {
            EventController.TriggerEvent(Consts.SELL_EVENT, 2);
            FunctionContainerRef.DoNotReset();
        }
    }
}
