using UnityEngine;

[CreateAssetMenu(fileName = "UIControllerConfig", menuName = "Game/UIControllerConfig")]
public class UIControllerConfig : ScriptableObject
{
    public int rows;
    public int columns;
    public Vector2 gridSize;
    public Vector2 gridPadding;
}