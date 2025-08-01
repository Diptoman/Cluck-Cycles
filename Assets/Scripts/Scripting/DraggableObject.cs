using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public class DraggableObject : MonoBehaviour
{
    public List<string> TargetSlotTag = new List<string>();

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
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (currentSlot)
        {
            //Add original offset
            transform.position = currentSlot.transform.position + currentSlot.SlotPlacementOffset;
            isInSlot = true;
            currentSlot.Occupy(); //End highlighting slot
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<SlotObject>(out SlotObject slotObj) && !isInSlot)
        {
            if (TargetSlotTag.Contains(slotObj.SlotTag))
            {
                if (currentSlot)
                {
                    currentSlot.EndHighlight(); //Remove highlight from previous slot
                }
                currentSlot = slotObj;
                currentSlot.StartHighlight();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<SlotObject>(out SlotObject slotObj))
        {
            if (slotObj == currentSlot)
            {
                currentSlot.EndHighlight(); //Remove highlight from slot
                currentSlot = null;
            }
        }
    }
}
