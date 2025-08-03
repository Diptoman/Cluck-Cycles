using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;
using TMPro;

public class SFXToggle : MonoBehaviour
{
    private Vector3 initialScale;

    void Start()
    {
        initialScale = this.transform.localScale;
    }

    void OnMouseDown()
    {
        if (GetComponent<TextMeshPro>().text == "SFX: On")
        {
            GetComponent<TextMeshPro>().text = "SFX: Off";
            MusicController.Instance.IsSFXOn = false;
        }
        else
        {
            GetComponent<TextMeshPro>().text = "SFX: On";
            MusicController.Instance.IsSFXOn = true;
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
}
