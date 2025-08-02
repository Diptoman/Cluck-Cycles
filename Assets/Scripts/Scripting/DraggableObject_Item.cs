
using UnityEngine;

public class DraggableObject_Item : DraggableObject
{
    public Vector3 InsideLoopOffset = new Vector3(2f, 0f, 0f);
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
                    SlotController.Instance.SetSlotStatus(currentSlot.GetSlotNumber(), SlotState.Reserved, AdditionalSlotsToReserve + AmountOfSlotsThisTakes);
                }

                //Assign new slot
                currentSlot = slotObj;
                SlotController.Instance.SetSlotStatus(currentSlot.GetSlotNumber(), SlotState.Highlighted, AdditionalSlotsToReserve + AmountOfSlotsThisTakes);
            }
        }

        if (slotObj.SlotTag == "ForEachSlot")
        {
            //If anything is selected, unhighlight it
            if (currentSlot)
            {
                SlotController.Instance.SetSlotStatus(currentSlot.GetSlotNumber(), SlotState.Reserved, AdditionalSlotsToReserve + AmountOfSlotsThisTakes);
            }
            currentSlot = slotObj;
            slotObj.SetState(SlotState.Highlighted);
        }

        if (slotObj.SlotTag == "InventorySlot")
        {
            InventoryController.CheckAndHighlightSlot(slotObj, this);
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

        if (slotObj.SlotTag == "InventorySlot")
        {
            InventoryController.CheckAndUnhighlightSlot(slotObj, this);
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

                //Add original offset
                Root.transform.position = slot.transform.position + slot.SlotPlacementOffset + InsideLoopOffset + new Vector3(0f, 0f, -.2f);
                Root.transform.SetParent(SlotController.Instance.GetLoopReference(slot.GetSlotNumber()).transform.parent);

                currentSlot = slot;
            }
        }

        if (slot.SlotTag == "ForEachSlot")
        {
            isInSlot = true;
            slot.SetState(SlotState.Highlighted);

            //Add original offset
            Root.transform.position = slot.transform.position + slot.SlotPlacementOffset + new Vector3(0f, 0f, -.2f);
            Root.transform.SetParent(slot.transform.parent);

            currentSlot = slot;
        }

        if (slot.SlotTag == "InventorySlot")
        {
            InventoryController.CheckAndAssignSlot(slot, this);
        }
    }

    protected override void CheckAndUnAssignSlot()
    {
        base.CheckAndUnAssignSlot();

        if (currentSlot)
        {
            if (currentSlot.SlotTag == "LoopSlot")
            {
                SlotController.Instance.SetSlotStatus(currentSlot.GetSlotNumber(), SlotState.Reserved, AdditionalSlotsToReserve + AmountOfSlotsThisTakes);
            }

            if (currentSlot.SlotTag == "ForEachSlot")
            {
                currentSlot.SetState(SlotState.Unoccupied);
            }

            if (currentSlot.SlotTag == "InventorySlot")
            {
                InventoryController.CheckAndUnAssignSlot(currentSlot, this);
            }
        }
    }
}
