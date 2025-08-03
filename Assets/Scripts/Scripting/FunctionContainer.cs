using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FunctionContainer : MonoBehaviour
{
    public ItemType itemType;
    public List<Actions> Functions = new List<Actions>();
    public TextMeshPro TextField;
    public FunctionSelector LeftSelector;
    public FunctionSelector RightSelector;
    public int currentIndex = 0;

    void Start()
    {
        TextField.text = Functions[0].ToString() + "()";
        ShowSelector(false);
    }

    public void AddIndex(int num)
    {
        currentIndex += num;
        currentIndex = Mathf.Clamp(currentIndex, 0, Functions.Count - 1);
        TextField.text = Functions[currentIndex].ToString() + "()";
    }

    public void ShowSelector(bool show)
    {
        TextField.gameObject.SetActive(show);
        LeftSelector.gameObject.SetActive(show);
        RightSelector.gameObject.SetActive(show);
    }

    public ItemType GetSelectedItemType()
    {
        return itemType;
    }

    public Actions GetSelectedAction()
    {
        return Functions[currentIndex];
    }
}

public enum Actions
{
    Cluck,
    LayEggs,
    Sell,
    Fertilize,
    Incubate,
    Cook
}
