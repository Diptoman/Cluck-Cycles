using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;
using TMPro;

public class MusicToggle : MonoBehaviour
{
    private Vector3 initialScale;

    void Start()
    {
        initialScale = this.transform.localScale;
    }

    void OnMouseDown()
    {
        if (GetComponent<TextMeshPro>().text == "Music: On")
        {
            GetComponent<TextMeshPro>().text = "Music: Off";
            MusicController.Instance.IsMusicOn = false;
            MusicController.Instance.TurnOffMusic();
        }
        else
        {
            GetComponent<TextMeshPro>().text = "Music: On";
            MusicController.Instance.IsMusicOn = true;
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
