using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Events;

public class FunctionContainer : MonoBehaviour
{
    public DraggableObject_Item DraggerRef;
    public ItemType itemType;
    public List<Actions> Functions = new List<Actions>();
    public TextMeshPro TextField;
    public FunctionSelector LeftSelector;
    public FunctionSelector RightSelector;
    public TextMeshPro AmountText;
    public TextMeshPro XText;
    public int currentIndex = 0;
    public int ItemInLoopAmount = 1;
    private int OriginalItemInLoopAmount = 1;

    UnityAction resetListener;


    void Start()
    {
        TextField.text = Functions[0].ToString() + "()";
        ShowSelector(false);

        //Bind to boss death
        resetListener = new UnityAction(OnReset);
        EventController.StartListening("CPUReset", resetListener);

    }

    public void OnReset()
    {
        ItemInLoopAmount = OriginalItemInLoopAmount;
        AmountText.text = ItemInLoopAmount.ToString();
    }

    void Update()
    {
        //Architecture fucked lmao
        /*DraggableObject_Loop loop = DraggerRef.GetLoopReference();
        if (loop != null)
        {
            if (!CluckController.IsProcessing)
            {
                ItemInLoopAmount = Mathf.Min(loop.GetLoopCount(), InventoryController.GetItemCount(itemType));
            }
        }
        else
        {
            ItemInLoopAmount = 0;
        }
        AmountText.text = ItemInLoopAmount.ToString();*/
    }

    public void DecrementItemCount()
    {
        ItemInLoopAmount--;
        AmountText.text = ItemInLoopAmount.ToString();
    }

    public void AddIndex(int num)
    {
        currentIndex += num;
        currentIndex = Mathf.Clamp(currentIndex, 0, Functions.Count - 1);
        TextField.text = Functions[currentIndex].ToString() + "()";
    }

    public void ShowSelector(bool show)
    {
        TextField.gameObject.SetActive(show);
        LeftSelector.gameObject.SetActive(show);
        RightSelector.gameObject.SetActive(show);
        AmountText.gameObject.SetActive(show);
        XText.gameObject.SetActive(show);
    }

    public ItemType GetSelectedItemType()
    {
        return itemType;
    }

    public Actions GetSelectedAction()
    {
        return Functions[currentIndex];
    }

    public void SetUsableCount()
    {
        DraggableObject_Loop loop = DraggerRef.GetLoopReference();
        ItemInLoopAmount = Mathf.Min(loop.GetLoopCount(), InventoryController.GetItemCount(itemType));
        OriginalItemInLoopAmount = ItemInLoopAmount;
        InventoryController.SetItemCount(itemType, InventoryController.GetItemCount(itemType) - ItemInLoopAmount);
        AmountText.text = ItemInLoopAmount.ToString();
    }

    public void ResetItemCount()
    {
        InventoryController.SetItemCount(itemType, InventoryController.GetItemCount(itemType) + OriginalItemInLoopAmount);
        ItemInLoopAmount = 0;
    }
}

public enum Actions
{
    Cluck,
    LayEggs,
    Sell,
    Fertilize,
    Incubate,
    Cook
}
