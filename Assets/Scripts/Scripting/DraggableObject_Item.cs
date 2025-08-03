using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using PrimeTween;
using UnityEngine;
using UnityEngine.Events;

public class DraggableObject_Item : DraggableObject
{
    public Vector3 InsideLoopOffset = new Vector3(2f, 0f, 0f);
    public FunctionContainer FunctionContainer;
    public ItemType itemType;

    protected override void CheckAndHighlightSlot(SlotObject slotObj)
    {
        base.CheckAndHighlightSlot(slotObj);

        if (slotObj.SlotTag == "LoopSlot")
        {
            if (SlotController.Instance.AreSlotsReserved(slotObj.GetSlotNumber(), AdditionalSlotsToReserve + AmountOfSlotsThisTakes))
            {
                //Remove the existing assigned slot
                if (currentSlot)
                {
                    if (currentSlot.SlotTag == "LoopSlot")
                    {
                        SlotController.Instance.SetSlotStatus(currentSlot.GetSlotNumber(), SlotState.Reserved, AdditionalSlotsToReserve + AmountOfSlotsThisTakes);
                    }
                    else if (currentSlot.SlotTag == "ForEachSlot")
                    {
                        currentSlot.SetState(SlotState.Unoccupied);
                    }
                }

                //Assign new slot
                currentSlot = slotObj;
                SlotController.Instance.SetSlotStatus(currentSlot.GetSlotNumber(), SlotState.Highlighted, AdditionalSlotsToReserve + AmountOfSlotsThisTakes);
            }
        }

        if (slotObj.SlotTag == "ForEachSlot" && (slotObj.GetSlotState() != SlotState.Occupied))
        {
            //If anything is selected, unhighlight it
            if (currentSlot)
            {
                SlotController.Instance.SetSlotStatus(currentSlot.GetSlotNumber(), SlotState.Reserved, AdditionalSlotsToReserve + AmountOfSlotsThisTakes);
            }
            currentSlot = slotObj;
            slotObj.SetState(SlotState.Highlighted);
        }
    }

    protected override void CheckAndUnhighlightSlot(SlotObject slotObj)
    {
        base.CheckAndUnhighlightSlot(slotObj);

        if (slotObj.SlotTag == "LoopSlot")
        {
            if (SlotController.Instance.GetLoopReference(currentSlot.GetSlotNumber()) != null)
            {
                SlotController.Instance.SetSlotStatus(currentSlot.GetSlotNumber(), SlotState.Reserved, AdditionalSlotsToReserve + AmountOfSlotsThisTakes);
            }
            else
            {
                SlotController.Instance.SetSlotStatus(currentSlot.GetSlotNumber(), SlotState.Unoccupied, AdditionalSlotsToReserve + AmountOfSlotsThisTakes);
            }
        }
        if (slotObj.SlotTag == "ForEachSlot")
        {
            slotObj.SetState(SlotState.Unoccupied);
        }
        currentSlot = null;
    }

    protected override void CheckAndAssignSlot(SlotObject slot)
    {
        base.CheckAndAssignSlot(slot);

        if (slot.SlotTag == "LoopSlot")
        {
            if (SlotController.Instance.AreSlotsHighlighted(slot.GetSlotNumber(), AdditionalSlotsToReserve + AmountOfSlotsThisTakes))
            {
                isInSlot = true;
                SlotController.Instance.SetSlotStatus(slot.GetSlotNumber(), SlotState.Occupied, AmountOfSlotsThisTakes); //Occupy
                SlotController.Instance.SetSlotStatus(slot.GetSlotNumber() + AmountOfSlotsThisTakes, SlotState.Reserved, AdditionalSlotsToReserve); //Reserve

                Parent.transform.position = slot.transform.position + slot.SlotPlacementOffset + InsideLoopOffset + new Vector3(0f, 0f, -.2f);
                //Add original offset
                /*if (slot.GetSlotNumber() % 2 == 1)
                {
                    Parent.transform.position = slot.transform.position + slot.SlotPlacementOffset + InsideLoopOffset + new Vector3(0f, 0f, -.2f);
                }
                else
                {
                    Parent.transform.position = slot.transform.position + slot.SlotPlacementOffset + InsideLoopOffset + new Vector3(-.5f, 0f, -.2f);
                }*/

                Parent.transform.SetParent(SlotController.Instance.GetLoopReference(slot.GetSlotNumber()).transform.parent);
                SlotController.Instance.GetLoopReference(slot.GetSlotNumber()).SetActiveItem(slot.GetSlotNumber(), this); //Add this to loop

                SetCurrentSlot(slot);
                FunctionContainer.SetUsableCount();

                FunctionContainer.ShowSelector(true);
            }
        }

        if (slot.SlotTag == "ForEachSlot")
        {
            isInSlot = true;
            slot.SetState(SlotState.Occupied);

            //Add original offset
            Parent.transform.position = slot.transform.position + slot.SlotPlacementOffset + new Vector3(0f, 0f, -.2f);
            Parent.transform.SetParent(slot.transform.parent);

            slot.transform.parent.GetComponent<DraggerPointer>().DraggerRef.SetForEachAttachedItem(this);

            currentSlot = slot;
        }
    }

    protected override void CheckAndUnAssignSlot()
    {
        base.CheckAndUnAssignSlot();

        if (currentSlot)
        {
            if (currentSlot.SlotTag == "LoopSlot")
            {
                FunctionContainer.ResetItemCount();
                SlotController.Instance.GetLoopReference(currentSlot.GetSlotNumber()).SetActiveItem(currentSlot.GetSlotNumber(), null); //Remove this from loop
                SlotController.Instance.SetSlotStatus(currentSlot.GetSlotNumber(), SlotState.Reserved, AdditionalSlotsToReserve + AmountOfSlotsThisTakes);

                FunctionContainer.ShowSelector(false);
            }

            if (currentSlot.SlotTag == "ForEachSlot")
            {
                currentSlot.SetState(SlotState.Unoccupied);
                currentSlot.transform.parent.GetComponent<DraggerPointer>().DraggerRef.SetForEachAttachedItem(null);
            }
        }
    }

    public void SetCurrentSlot(SlotObject slot)
    {
        currentSlot = slot;
        if (slot == null)
        {
            return;
        }
        else
        {
            isInSlot = true;
        }


        /*if (slot.SlotTag == "LoopSlot")
        {
            //Add original offset
            Parent.transform.SetParent(null);
            if (slot.GetSlotNumber() % 2 == 1)
            {
                Parent.transform.position = slot.transform.position + slot.SlotPlacementOffset + InsideLoopOffset + new Vector3(0f, 0f, -.2f);
            }
            else
            {
                Parent.transform.position = slot.transform.position + slot.SlotPlacementOffset + InsideLoopOffset + new Vector3(-.5f, 0f, -.2f);
            }
            Parent.transform.SetParent(SlotController.Instance.GetLoopReference(slot.GetSlotNumber()).transform.parent);
        }*/
    }

    public void Process()
    {
        this.gameObject.GetComponent<LoopItem>().Process();
        DecrementItemInLoop();
    }

    public DraggableObject_Loop GetLoopReference()
    {
        if (currentSlot && currentSlot.SlotTag == "LoopSlot")
        {
            return SlotController.Instance.GetLoopReference(currentSlot.GetSlotNumber());
        }

        return null;
    }

    public void DecrementItemInLoop()
    {
        FunctionContainer.DecrementItemCount();
    }

    public int GetItemsInLoopRemaining()
    {
        return FunctionContainer.ItemInLoopAmount;
    }

    public void ResetItemCount()
    {
        FunctionContainer.ResetItemCount();
        FunctionContainer.SetUsableCount();
    }

    public void Cleanup()
    {
        FunctionContainer.ResetItemCount();
        Destroy(transform.parent.gameObject);
    }
}
