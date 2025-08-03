using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UIControllerConfig", menuName = "Game/UIControllerConfig")]
public class UIControllerConfig : ScriptableObject
{
    [Header("Sprites")]
    public ItemInfo[] itemSprites;

    [Header("Inventory")]
    public int rows;
    public int columns;
    public Vector2 gridSize;
    public Vector2 gridPadding;

    [Header("Shop")]
    public int shopRows;
    public int shopColumns;
    public Vector2 shopGridSize;
    public Vector2 shopGridPadding;

    // Mouse Enter Animation
    public MousePointAnimation.Config pointAnimConfig;
}