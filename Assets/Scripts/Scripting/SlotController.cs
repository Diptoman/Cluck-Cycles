using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlotController : MonoBehaviour
{
    #region Singleton script
    public static SlotController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Debug.Log($"Init instance of Slot controller");
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log($"Destroy instance of Slot controller");
            Destroy(gameObject);
        }
    }

    public static void Destroy()
    {
        Instance = null;
    }
    #endregion

    public SlotObject SlotPrefab;
    public Vector3 StartLocation;
    public float SlotGap = .6f;
    public int numSlots = 20;
    public UIController uIControllerRef;

    private Dictionary<int, SlotState> SlotStates = new Dictionary<int, SlotState>();
    private Dictionary<int, SlotObject> SlotObjects = new Dictionary<int, SlotObject>();
    private Dictionary<int, DraggableObject_Loop> LoopReference = new Dictionary<int, DraggableObject_Loop>();

    void Start()
    {
        for (int i = 0; i < numSlots; i++)
        {
            SlotObject slot = Instantiate(SlotPrefab, StartLocation + new Vector3(0f, -i * SlotGap, 0f), Quaternion.identity);
            slot.Init(i);
            SlotStates[i] = SlotState.Unoccupied;
            SlotObjects[i] = slot;
        }

        // var slotCount = SlotObjects.Count;

        // foreach (var inventorySlot in uIControllerRef.itemSlots)
        // {
        //     var slot = inventorySlot.slotObject;
        //     slot.Init(slotCount);
        //     SlotStates[slotCount] = SlotState.Unoccupied;
        //     SlotObjects[slotCount] = slot;

        //     slotCount++;
        // }
    }

    public void SetSlotStatus(int num, SlotState state, int slotAmount = 1)
    {
        for (int i = num; i < num + slotAmount; i++)
        {
            SlotStates[i] = state;
            SlotObjects[i].SetState(state);
        }
    }

    public SlotState GetSlotStatus(int num)
    {
        return SlotStates[num];
    }

    public bool GetSlotAvailability(int startingNum, int totalSlots)
    {
        for (int i = startingNum; i < startingNum + totalSlots; i++)
        {
            if (i >= SlotStates.Count)
                return false;

            if (SlotStates[i] == SlotState.Occupied || SlotStates[i] == SlotState.Reserved)
                return false;
        }

        return true;
    }

    public bool AreSlotsReserved(int startingNum, int totalSlots)
    {
        for (int i = startingNum; i < startingNum + totalSlots; i++)
        {
            if (i >= SlotStates.Count)
                return false;

            if (SlotStates[i] != SlotState.Reserved)
                return false;
        }

        return true;
    }

    public bool AreSlotsHighlighted(int startingNum, int totalSlots)
    {
        for (int i = startingNum; i < startingNum + totalSlots; i++)
        {
            if (i >= SlotStates.Count)
                return false;

            if (SlotStates[i] != SlotState.Highlighted)
                return false;
        }

        return true;
    }

    public void SetLoopReference(int startingIndex, int length, DraggableObject_Loop loopRef)
    {
        for (int i = startingIndex; i < (startingIndex + length); i++)
        {
            LoopReference[i] = loopRef;
        }
    }

    public DraggableObject_Loop GetLoopReference(int index)
    {
        return LoopReference[index];
    }
}
