using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public class DraggableObject : MonoBehaviour
{
    public List<string> TargetSlotTag = new List<string>();
    public int AmountOfSlotsThisTakes = 1, AdditionalSlotsToReserve = 0;

    private bool isDragging = false, isInSlot = false;
    private Vector3 dragOffset, initialPosition, initialScale;
    private SlotObject currentSlot;

    void Start()
    {
        initialPosition = this.transform.position;
        initialScale = this.transform.localScale;
    }

    void Update()
    {
        if (isDragging)
        {
            //Add original offset
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + dragOffset;
        }
        else
        {
            if (!isInSlot)
            {
                transform.position = initialPosition;
            }
        }
    }

    void OnMouseDown()
    {
        //Diff between centre and clicked point on plane
        dragOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
        isInSlot = false;

        if (currentSlot)
        {
            SlotController.Instance.SetSlotStatus(currentSlot.GetSlotNumber(), SlotState.Unoccupied, AdditionalSlotsToReserve + AmountOfSlotsThisTakes);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (currentSlot)
        {
            //Add original offset
            transform.position = currentSlot.transform.position + currentSlot.SlotPlacementOffset;

            if (SlotController.Instance.GetSlotAvailability(currentSlot.GetSlotNumber(), AdditionalSlotsToReserve + AmountOfSlotsThisTakes))
            {
                isInSlot = true;
                SlotController.Instance.SetSlotStatus(currentSlot.GetSlotNumber(), SlotState.Occupied, AmountOfSlotsThisTakes); //Occupy
                SlotController.Instance.SetSlotStatus(currentSlot.GetSlotNumber() + AmountOfSlotsThisTakes, SlotState.Reserved, AdditionalSlotsToReserve); //Reserve
            }
        }
    }

    void OnMouseEnter()
    {
        Tween.Scale(this.transform, initialScale * 1.2f, .25f);
    }

    void OnMouseExit()
    {
        Tween.Scale(this.transform, initialScale, .25f);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!currentSlot)
        {
            if (other.gameObject.TryGetComponent<SlotObject>(out SlotObject slotObj) && !isInSlot)
            {
                if (TargetSlotTag.Contains(slotObj.SlotTag))
                {
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
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<SlotObject>(out SlotObject slotObj) && !isInSlot)
        {
            if (TargetSlotTag.Contains(slotObj.SlotTag))
            {
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
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<SlotObject>(out SlotObject slotObj))
        {
            if (slotObj == currentSlot)
            {
                SlotController.Instance.SetSlotStatus(currentSlot.GetSlotNumber(), SlotState.Unoccupied, AdditionalSlotsToReserve + AmountOfSlotsThisTakes);
                currentSlot = null;
            }
        }
    }
}
