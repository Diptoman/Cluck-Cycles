using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopItem : MonoBehaviour
{
    public FunctionContainer FunctionContainerRef;
    public virtual void Process()
    {
        CluckController.Process(FunctionContainerRef.GetSelectedItemType(), FunctionContainerRef.GetSelectedAction());
    }
}
