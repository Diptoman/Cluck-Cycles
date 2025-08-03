using Sirenix.OdinInspector;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [InlineEditor]
    public UIControllerConfig config;
    public UIInventorySlot[] itemSlots;
    public SpriteRenderer inventoryBase;
    public Transform slotsParent;
    public TooltipBox tooltip;
    // Start is called before the first frame update
    void Start()
    {
        InitItems();
        ArrangeItems();
        InventoryController.Init(this);
        Instance = this;
        tooltip.Hide(isForced: true);

        //Test, remove later
        InventoryController.SetItemCount(ItemType.Chicken, 3);
        InventoryController.SetItemCount(ItemType.Egg, 16);
    }

    // Update is called once per frame
    void Update()
    {
        ArrangeItems();
    }

    private void InitItems()
    {
        var slots = inventoryBase.GetComponentsInChildren<UIInventorySlot>();
        itemSlots = slots;

        var parentX = -inventoryBase.size.x / 2 + config.gridSize.x / 2;
        var parentY = -inventoryBase.size.y / 2 + config.gridSize.y / 2;
        var z = slotsParent.transform.localPosition.z;
        slotsParent.transform.localPosition = new Vector3(parentX, parentY, z);

        for (var i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].SetStackSize(0);
            var isValid = i < (int)ItemType.MAX;

            if (isValid)
            {
                itemSlots[i].itemType = (ItemType)i;
                itemSlots[i].slotSprite.sprite = itemSlots[i].sprite;
            }
            else
            {
                itemSlots[i].itemType = ItemType.Invalid;
            }
        }
    }

    private void ArrangeItems()
    {
        var inventorySize = inventoryBase.size - config.gridPadding;
        config.gridSize.x = inventorySize.x / config.columns;
        config.gridSize.y = inventorySize.y / config.rows;

        for (int i = 0; i < config.rows; i++)
        {
            for (int j = 0; j < config.columns; j++)
            {
                var index = i * config.columns + j;
                if (index >= itemSlots.Length)
                    continue;

                var slot = itemSlots[index];
                var tf = slot.transform;
                var x = j * config.gridSize.x + config.gridPadding.x / 2;
                var y = (config.rows - 1 - i) * config.gridSize.y + config.gridPadding.y / 2;

                tf.SetPosXYLocal(x, y);
                slot.slotBox.size = config.gridSize;
            }
        }
    }

    [Button("Setup slots")]
    void SetupStuff()
    {
        InitItems();
        ArrangeItems();
    }
}
