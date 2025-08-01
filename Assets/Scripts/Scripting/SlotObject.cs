using System.Collections;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

public class SlotObject : MonoBehaviour
{
    public string SlotTag;
    public Vector3 SlotPlacementOffset;
    public Color DefaultColour, HighlightColour, OccupyColour;

    private bool isOccupied = false;

    void Start()
    {
        Tween.Color(this.gameObject.GetComponent<SpriteRenderer>(), DefaultColour, 0f);
    }

    public void StartHighlight()
    {
        Tween.Color(this.gameObject.GetComponent<SpriteRenderer>(), HighlightColour, .1f);
    }

    public void EndHighlight()
    {
        Tween.Color(this.gameObject.GetComponent<SpriteRenderer>(), DefaultColour, .1f);
    }

    public void Occupy()
    {
        Tween.Color(this.gameObject.GetComponent<SpriteRenderer>(), OccupyColour, .25f);
        isOccupied = true;
    }
}
