using System.Collections;
using System.Collections.Generic;
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
    }

    protected override void CheckAndUnhighlightSlot(SlotObject slotObj)
    {
        base.CheckAndUnhighlightSlot(slotObj);

        SlotController.Instance.SetSlotStatus(currentSlot.GetSlotNumber(), SlotState.Reserved, AdditionalSlotsToReserve + AmountOfSlotsThisTakes);
        currentSlot = null;
    }

    protected override void CheckAndAssignSlot(SlotObject slot)
    {
        base.CheckAndAssignSlot(slot);

        if (slot.SlotTag == "LoopSlot")
        {
            Debug.Log(SlotController.Instance.GetSlotStatus(slot.GetSlotNumber()));
            if (SlotController.Instance.AreSlotsHighlighted(slot.GetSlotNumber(), AdditionalSlotsToReserve + AmountOfSlotsThisTakes))
            {
                isInSlot = true;
                SlotController.Instance.SetSlotStatus(slot.GetSlotNumber(), SlotState.Occupied, AmountOfSlotsThisTakes); //Occupy
                SlotController.Instance.SetSlotStatus(slot.GetSlotNumber() + AmountOfSlotsThisTakes, SlotState.Reserved, AdditionalSlotsToReserve); //Reserve

                //Add original offset
                transform.position = slot.transform.position + slot.SlotPlacementOffset + InsideLoopOffset;

                lastAssignedSlot = slot;
                currentSlot = slot;
            }
        }
    }

    protected override void CheckAndUnAssignSlot()
    {
        base.CheckAndUnAssignSlot();

        if (currentSlot)
        {
            SlotController.Instance.SetSlotStatus(currentSlot.GetSlotNumber(), SlotState.Unoccupied, AdditionalSlotsToReserve + AmountOfSlotsThisTakes);
        }
    }
}
