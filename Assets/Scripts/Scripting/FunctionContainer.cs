using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FunctionContainer : MonoBehaviour
{
    public List<String> Functions = new List<string>();
    public TextMeshPro TextField;
    public FunctionSelector LeftSelector;
    public FunctionSelector RightSelector;
    public int currentIndex = 0;

    void Start()
    {
        TextField.text = Functions[0];
        ShowSelector(false);
    }

    public void AddIndex(int num)
    {
        currentIndex += num;
        currentIndex = Mathf.Clamp(currentIndex, 0, Functions.Count - 1);
        TextField.text = Functions[currentIndex];
    }

    public void ShowSelector(bool show)
    {
        TextField.gameObject.SetActive(show);
        LeftSelector.gameObject.SetActive(show);
        RightSelector.gameObject.SetActive(show);
    }
}
