using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public class ProcessingLine : MonoBehaviour
{
    Vector3 scale = new Vector3(.25f, .25f, .25f);

    void Start()
    {
        scale = transform.localScale;
    }

    public void Reset()
    {
        Tween.Scale(this.transform, scale * 1.2f, .1f);
        Tween.Scale(this.transform, scale, .1f, startDelay: .1f);
    }
}
