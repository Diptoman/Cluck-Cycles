using Sirenix.OdinInspector;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [InlineEditor]
    public UIControllerConfig config;
    public UIInventorySlot[] itemSlots;
    public UIShopSlot[] shopSlots;
    public SpriteRenderer inventoryBase;
    public SpriteRenderer shopBase;
    public Transform slotsParent;
    public Transform shopSlotsParent;
    public TooltipBox tooltip;
    // Start is called before the first frame update
    void Start()
    {
        Global.itemSprites = config.itemSprites;

        MousePointAnimation.globalConfig = config.pointAnimConfig;
        InitItems();
        ArrangeItems();
        InventoryController.Init(this);
        ShopController.Init(this);
        Instance = this;
        tooltip.Hide(isForced: true);


        //Test, remove later
        InventoryController.SetItemCount(ItemType.Chicken, 4);






        //More test stuff
        Global.Money = 30;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        MousePointAnimation.globalConfig = config.pointAnimConfig;
#endif
        ArrangeItems();
    }

    private void InitItems()
    {
        itemSlots = inventoryBase.GetComponentsInChildren<UIInventorySlot>(); ;
        shopSlots = shopBase.GetComponentsInChildren<UIShopSlot>();

        var parentX = -inventoryBase.size.x / 2 + config.gridSize.x / 2;
        var parentY = -inventoryBase.size.y / 2 + config.gridSize.y / 2;
        var z = slotsParent.transform.localPosition.z;
        slotsParent.transform.localPosition = new Vector3(parentX, parentY, z);

        parentX = -shopBase.size.x / 2 + config.shopGridSize.x / 2;
        parentY = -shopBase.size.y / 2 + config.shopGridSize.y / 2;
        z = shopSlotsParent.transform.localPosition.z;
        shopSlotsParent.transform.localPosition = new Vector3(parentX, parentY, z);
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
                slot.itemType = (ItemType)index;
                slot.slotSprite.sprite = Global.GetItemInfo(config.itemSprites, (ItemType)index).sprite;
            }
        }

        var shopSize = shopBase.size - config.shopGridPadding;
        config.shopGridSize.x = shopSize.x / config.shopColumns;
        config.shopGridSize.y = shopSize.y / config.shopRows;

        for (int i = 0; i < config.shopRows; i++)
        {
            for (int j = 0; j < config.shopColumns; j++)
            {
                var index = i * config.shopColumns + j;
                if (index >= shopSlots.Length)
                    continue;

                var slot = shopSlots[index];
                var tf = slot.transform;
                var x = j * config.shopGridSize.x + config.shopGridPadding.x / 2;
                var y = (config.shopRows - 1 - i) * config.shopGridSize.y + config.shopGridPadding.y / 2;

                tf.SetPosXYLocal(x, y);
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
