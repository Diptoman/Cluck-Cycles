using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

public class SlotObject : MonoBehaviour
{
    public string SlotTag;
    public Vector3 SlotPlacementOffset;
    public Color DefaultColour, HighlightColour, OccupyColour, ReservedColour;

    private SlotState state = SlotState.Unoccupied;
    private int slotNum = -1;
    private SlotCreator slotCreator;

    void Start()
    {
        Tween.Color(this.gameObject.GetComponent<SpriteRenderer>(), DefaultColour, 0f);
    }

    public void StartHighlight()
    {
        if (state == SlotState.Unoccupied)
        {
            Tween.Color(this.gameObject.GetComponent<SpriteRenderer>(), HighlightColour, .1f);
            state = SlotState.Highlighted;
        }
    }

    public void EndHighlight()
    {
        if (state == SlotState.Highlighted)
        {
            Tween.Color(this.gameObject.GetComponent<SpriteRenderer>(), DefaultColour, .1f);
            state = SlotState.Unoccupied;
        }
    }

    public void Occupy()
    {
        if (state != SlotState.Occupied)
        {
            Tween.Color(this.gameObject.GetComponent<SpriteRenderer>(), OccupyColour, .25f);
            state = SlotState.Occupied;
        }
    }

    public void UnOccupy()
    {
        if (state == SlotState.Occupied)
        {
            Tween.Color(this.gameObject.GetComponent<SpriteRenderer>(), DefaultColour, .1f);
            state = SlotState.Unoccupied;
        }
    }

    public void Reserve()
    {
        if (state != SlotState.Reserved && state != SlotState.Occupied)
        {
            Tween.Color(this.gameObject.GetComponent<SpriteRenderer>(), ReservedColour, .25f);
            state = SlotState.Reserved;
        }
    }

    public void Init(int num, SlotCreator creatorReference)
    {
        slotNum = num;
        slotCreator = creatorReference;
    }

    public SlotState GetSlotState()
    {
        return state;
    }
}

public enum SlotState
{
    Unoccupied,
    Highlighted,
    Occupied,
    Reserved
}
