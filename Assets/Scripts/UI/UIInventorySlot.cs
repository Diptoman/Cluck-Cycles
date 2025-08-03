using UnityEngine;
using TMPro;

public class UIInventorySlot : MonoBehaviour
{
    public ItemType itemType;
    public Sprite sprite;
    public int stackSize { get; private set; }
    public SpriteRenderer slotBox;
    public SpriteRenderer slotSprite;
    public FloatAnimation floatAnim;
    public FloatAnimation floatAnimText;
    public TextMeshPro text;
    public Color activeTextColor;
    public GameObject itemToSpawn;

    public void SetStackSize(int size)
    {
        stackSize = size;
        text.SetText("x" + stackSize);

        if (size > 0)
        {
            floatAnim?.SetActive(true);
            floatAnimText?.SetActive(true);
            slotSprite.color = Color.white;
            text.color = activeTextColor;
        }
        else
        {
            floatAnim?.SetActive(false);
            floatAnimText?.SetActive(false);
            slotSprite.color = Color.gray;
            text.color = Color.gray;
        }
    }

    private DraggableObject_Item draggedItem;
    void OnMouseDown()
    {
        var pos = transform.position;
        pos.z = -3f;
        var item = GameObject.Instantiate(itemToSpawn, pos, Quaternion.identity);
        var draggable = item.GetComponentInChildren<DraggableObject_Item>();
        draggedItem = draggable;
        draggable.SpawnFromInventory();
    }

    void OnMouseUp()
    {
        draggedItem.DropFromInventory();
    }
}
