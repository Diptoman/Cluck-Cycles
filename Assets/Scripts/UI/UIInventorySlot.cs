using UnityEngine;
using TMPro;

public class UIInventorySlot : MonoBehaviour
{
    public int stackSize { get; private set; }
    public SpriteRenderer slotBox;
    public SpriteRenderer slotSprite;
    public FloatAnimation floatAnim;
    public TextMeshPro text;
    public DraggableObject_Item itemToSpawn;
    public Color activeTextColor;

    public void SetStackSize(int size)
    {
        stackSize = size;
        text.SetText("x" + stackSize);

        if (size > 0)
        {
            floatAnim.SetActive(true);
            slotSprite.color = Color.white;
            text.color = activeTextColor;
        }
        else
        {
            floatAnim.SetActive(false);
            slotSprite.color = Color.gray;
            text.color = Color.gray;
        }
    }
}
