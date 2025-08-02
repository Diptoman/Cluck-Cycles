using Sirenix.OdinInspector;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [InlineEditor]
    public UIControllerConfig config;
    public Transform[] itemSlots;
    // Start is called before the first frame update
    void Start()
    {
        ArrangeItems();
    }

    // Update is called once per frame
    void Update()
    {
        ArrangeItems();
    }

    private void ArrangeItems()
    {
        for (int i = 0; i < config.rows; i++)
        {
            for (int j = 0; j < config.columns; j++)
            {
                var index = i * config.columns + j;
                if (index >= itemSlots.Length)
                    continue;
                var tf = itemSlots[index].transform;
                var x = j * config.gridSize.y;
                var y = i * config.gridSize.x;
                var z = tf.localPosition.z;

                tf.localPosition = new Vector3(x, y, z);
            }
        }
    }
}
