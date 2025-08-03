using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UIInventorySlot : MonoBehaviour
{
    public ItemType itemType;
    public int stackSize { get; private set; }
    public SpriteRenderer slotBox;
    public SpriteRenderer slotSprite;
    public FloatAnimation floatAnim;
    public FloatAnimation floatAnimText;
    public TextMeshPro text;
    public Color activeTextColor;

    UnityAction resetListener;

    void Start()
    {
        //Bind to Reset
        resetListener = new UnityAction(OnReset);
        EventController.StartListening("CPUReset", resetListener);
    }

    public void OnReset()
    {
        if (Global.GetItemInfo(itemType).isTransient)
        {
            SetStackSize(0);
        }
    }

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
        if (CluckController.IsProcessing)
            return;

        var pos = transform.position;
        pos.z = -3f;
        var itemToSpawn = Global.GetItemInfo(itemType).draggableItem;
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
