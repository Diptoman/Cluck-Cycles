using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private Vector3 initialScale;

    void Start()
    {
        initialScale = this.transform.localScale;
    }

    void OnMouseDown()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
