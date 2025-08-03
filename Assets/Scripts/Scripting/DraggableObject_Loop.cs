using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DraggableObject_Loop : DraggableObject
{
    private List<DraggableObject_Item> itemList = new List<DraggableObject_Item>();
    private int maxLoop = 19;
    public int LoopCount = 5;
    public bool isForEach = false;
    public TextMeshPro LoopCountText;
    private DraggableObject_Item forEachAttachedItem;

    protected override void Init()
    {
        base.Init();

        for (int i = 0; i < maxLoop; i++)
        {
            itemList.Add(null);
        }

        if (LoopCountText)
        {
            LoopCountText.text = LoopCount.ToString();
        }
    }

    protected override void CheckAndHighlightSlot(SlotObject slotObj)
    {
        base.CheckAndHighlightSlot(slotObj);

        if (SlotController.Instance.GetSlotAvailability(slotObj.GetSlotNumber(), AdditionalSlotsToReserve + AmountOfSlotsThisTakes))
        {
            //Remove the existing assigned slot
            if (currentSlot)
            {
                SlotController.Instance.SetSlotStatus(currentSlot.GetSlotNumber(), SlotState.Unoccupied, AdditionalSlotsToReserve + AmountOfSlotsThisTakes);
            }

            //Assign new slot
            currentSlot = slotObj;
            SlotController.Instance.SetSlotStatus(currentSlot.GetSlotNumber(), SlotState.Highlighted, AdditionalSlotsToReserve + AmountOfSlotsThisTakes);
        }
    }

    protected override void CheckAndUnhighlightSlot(SlotObject slotObj)
    {
        base.CheckAndUnhighlightSlot(slotObj);

        if (currentSlot == null)
            return;

        SlotController.Instance.SetSlotStatus(currentSlot.GetSlotNumber(), SlotState.Unoccupied, AdditionalSlotsToReserve + AmountOfSlotsThisTakes);
        currentSlot = null;
    }

    protected override void CheckAndAssignSlot(SlotObject slot)
    {
        base.CheckAndAssignSlot(slot);

        if (SlotController.Instance.GetSlotAvailability(slot.GetSlotNumber(), AdditionalSlotsToReserve + AmountOfSlotsThisTakes))
        {
            isInSlot = true;
            SlotController.Instance.SetSlotStatus(slot.GetSlotNumber(), SlotState.Occupied, AmountOfSlotsThisTakes); //Occupy

            SlotController.Instance.SetLoopReference(slot.GetSlotNumber(), AmountOfSlotsThisTakes + AdditionalSlotsToReserve, this); //Set loop reference

            //Go through its list and occupy the slots that were occupied before drag
            for (int i = 1; i <= AdditionalSlotsToReserve; i++)
            {
                if (itemList[i] != null)
                {
                    SlotController.Instance.SetSlotStatus(slot.GetSlotNumber() + AmountOfSlotsThisTakes + i - 1, SlotState.Occupied, 1); //Occupy
                    itemList[i].SetCurrentSlot(SlotController.Instance.GetSlotReference(slot.GetSlotNumber() + AmountOfSlotsThisTakes + i - 1));
                }
                else
                {
                    SlotController.Instance.SetSlotStatus(slot.GetSlotNumber() + AmountOfSlotsThisTakes + i - 1, SlotState.Reserved, 1); //Reserve
                }
            }

            //Add original offset
            Parent.transform.position = slot.transform.position + slot.SlotPlacementOffset;

            lastAssignedSlot = slot;
            currentSlot = slot;
        }
    }

    protected override void CheckAndUnAssignSlot()
    {
        base.CheckAndUnAssignSlot();

        if (currentSlot)
        {
            SlotController.Instance.SetSlotStatus(currentSlot.GetSlotNumber(), SlotState.Unoccupied, AdditionalSlotsToReserve + AmountOfSlotsThisTakes);
            SlotController.Instance.SetLoopReference(currentSlot.GetSlotNumber(), AmountOfSlotsThisTakes + AdditionalSlotsToReserve, null);

            //Go through its list and occupy the slots that were occupied before drag
            for (int i = 1; i <= AdditionalSlotsToReserve; i++)
            {
                if (itemList[i] != null)
                {
                    itemList[i].SetCurrentSlot(null);
                }
            }
        }
    }

    public void SetActiveItem(int globalSlotNum, DraggableObject_Item item)
    {
        int currentSlotNum = currentSlot.GetSlotNumber(); //Current slot num

        int index = globalSlotNum - currentSlotNum;

        itemList[index] = item;
    }

    public DraggableObject_Item GetItem(int lineInLoop)
    {
        return itemList[lineInLoop];
    }

    public void SetForEachAttachedItem(DraggableObject_Item item)
    {
        forEachAttachedItem = item;
        foreach (DraggableObject_Item attachedItem in itemList)
        {
            if (attachedItem != null)
            {
                attachedItem.ResetItemCount();
            }
        }
    }

    public void SetLoopCount(int count)
    {
        //Set, check if free etc.
    }

    public int GetLoopCount()
    {
        if (!isForEach)
            return LoopCount;
        else
        {
            if (forEachAttachedItem != null)
            {
                return InventoryController.GetItemCount(forEachAttachedItem.itemType);
            }
        }

        return 0;
    }

    public int GetCluckCount()
    {
        int totalActions = 0;
        foreach (DraggableObject_Item item in itemList)
        {
            if (item != null)
                totalActions++;
        }

        return totalActions * GetLoopCount();
    }
}
