using Sirenix.OdinInspector;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [InlineEditor]
    public UIControllerConfig config;
    public UIInventorySlot[] itemSlots;
    public SpriteRenderer inventoryBase;
    public Transform slotsParent;
    // Start is called before the first frame update
    void Start()
    {
        InitItems();
        ArrangeItems();
        InventoryController.Init(this);
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
                var y = i * config.gridSize.y + config.gridPadding.y / 2;
                var z = tf.localPosition.z;

                tf.localPosition = new Vector3(x, y, z);
                slot.slotBox.size = config.gridSize;
                // slot.boxCollider.size = config.gridSize;
            }
        }
    }
}
