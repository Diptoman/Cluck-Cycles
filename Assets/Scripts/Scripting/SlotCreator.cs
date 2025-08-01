using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotCreator : MonoBehaviour
{
    public SlotObject SlotPrefab;
    public Vector3 StartLocation;
    public float SlotGap = .6f;
    public int numSlots = 20;
    void Start()
    {
        for (int i = 0; i < numSlots; i++)
        {
            SlotObject slot = Instantiate(SlotPrefab, StartLocation + new Vector3(0f, -i * SlotGap, 0f), Quaternion.identity);
            slot.Init(i, this);
        }
    }
}
