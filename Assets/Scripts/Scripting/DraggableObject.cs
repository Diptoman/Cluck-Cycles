using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public class DraggableObject : MonoBehaviour
{
    public bool destroyIfNotInSlot;
    public GameObject Parent;
    public List<string> TargetSlotTag = new List<string>();
    public int AmountOfSlotsThisTakes = 1, AdditionalSlotsToReserve = 0;

    protected bool isDragging = false, isInSlot = false;
    public Vector3 dragOffset, initialPosition, initialScale;
    public SlotObject currentSlot, lastAssignedSlot;

    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        initialPosition = this.transform.position;
        initialScale = this.transform.localScale;
    }

    public void SpawnFromInventory()
    {
        OnMouseDown();
    }

    public void DropFromInventory()
    {
        OnMouseUp();
    }

    void Update()
    {
        if (isDragging)
        {
            //Add original offset
            Parent.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + dragOffset;
        }
        else
        {
            if (!isInSlot)
            {
                Parent.transform.position = initialPosition;
                if (destroyIfNotInSlot)
                {
                    GameObject.Destroy(this.gameObject);
                }
            }
        }
    }

    void OnMouseDown()
    {
        //Diff between centre and clicked point on plane
        dragOffset = Parent.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
        isInSlot = false;

        CheckAndUnAssignSlot();
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (currentSlot)
        {
            CheckAndAssignSlot(currentSlot);
        }
        else if (lastAssignedSlot)
        {
            CheckAndAssignSlot(lastAssignedSlot);
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
                    CheckAndHighlightSlot(slotObj);
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
                CheckAndHighlightSlot(slotObj);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<SlotObject>(out SlotObject slotObj))
        {
            if (slotObj == currentSlot)
            {
                CheckAndUnhighlightSlot(slotObj);
            }
        }
    }

    protected virtual void CheckAndHighlightSlot(SlotObject slotObj) { }

    protected virtual void CheckAndUnhighlightSlot(SlotObject slotObj) { }

    protected virtual void CheckAndAssignSlot(SlotObject slotObj) { }

    protected virtual void CheckAndUnAssignSlot() { }
}
