using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using TMPro;
using UnityEngine;

public class SlotObject : MonoBehaviour
{
    public string SlotTag;
    public Vector3 SlotPlacementOffset;
    public Color DefaultColour, HighlightColour, OccupyColour, ReservedColour;
    public TextMeshPro SlotNumText;

    private SlotState state = SlotState.Unoccupied;
    private int slotNum = -1;

    void Start()
    {
        Tween.Color(this.gameObject.GetComponent<SpriteRenderer>(), DefaultColour, 0f);
    }

    public void SetState(SlotState slotstate)
    {
        state = slotstate;

        switch (state)
        {
            case SlotState.Unoccupied:
                Tween.Color(this.gameObject.GetComponent<SpriteRenderer>(), DefaultColour, .1f);
                break;

            case SlotState.Occupied:
                Tween.Color(this.gameObject.GetComponent<SpriteRenderer>(), OccupyColour, .1f);
                break;

            case SlotState.Highlighted:
                Tween.Color(this.gameObject.GetComponent<SpriteRenderer>(), HighlightColour, .1f);
                break;

            case SlotState.Reserved:
                Tween.Color(this.gameObject.GetComponent<SpriteRenderer>(), ReservedColour, .1f);
                break;
        }
    }

    public void Init(int num)
    {
        slotNum = num;

        if (SlotNumText)
        {
            SlotNumText.text = (slotNum + 1).ToString();
        }
    }

    public SlotState GetSlotState()
    {
        return state;
    }

    public int GetSlotNumber()
    {
        return slotNum;
    }
}

public enum SlotState
{
    Unoccupied,
    Highlighted,
    Occupied,
    Reserved
}
