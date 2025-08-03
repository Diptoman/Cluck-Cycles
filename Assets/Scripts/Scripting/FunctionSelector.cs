using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;
using TMPro;

public class FunctionSelector : MonoBehaviour
{
    public bool IsRight = false;
    public FunctionContainer FunctionContainer;

    private Vector3 initialScale;

    void Start()
    {
        initialScale = this.transform.localScale;
    }

    void OnMouseEnter()
    {
        Tween.Scale(this.transform, initialScale * 1.2f, .25f);
    }

    void OnMouseExit()
    {
        Tween.Scale(this.transform, initialScale, .25f);
    }

    void OnMouseDown()
    {
        if (CluckController.IsProcessing)
            return;

        if (IsRight)
        {
            FunctionContainer.AddIndex(1);
        }
        else
        {
            FunctionContainer.AddIndex(-1);
        }
    }
}
