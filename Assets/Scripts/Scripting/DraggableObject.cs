using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public class DraggableObject : MonoBehaviour
{
    public List<string> TargetSlotTag = new List<string>();

    private bool isDragging = false;
    private Vector3 offset;

    void Update()
    {
        if (isDragging)
        {
            //Add original offset
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;

        }
    }

    void OnMouseDown()
    {
        //Diff between centre and clicked point on plane
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void OnMouseEnter()
    {
        Tween.Scale(this.transform, 1.2f, .25f);
    }

    void OnMouseExit()
    {
        Tween.Scale(this.transform, 1f, .25f);
    }
}
