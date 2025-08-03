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
    public ProcessingLine ProcessingLineMarkerPrefab;
    public DraggerPointer ForEachLoopPrefab;
    public DraggerPointer ForLoopPrefab;
    private ProcessingLine spawnedMarker;

    private Dictionary<int, SlotState> SlotStates = new Dictionary<int, SlotState>();
    private Dictionary<int, SlotObject> SlotObjects = new Dictionary<int, SlotObject>();
    private Dictionary<int, DraggableObject_Loop> LoopReference = new Dictionary<int, DraggableObject_Loop>();

    void Start()
    {
        for (int i = 0; i < numSlots; i++)
        {
            SlotObject slot = Instantiate(SlotPrefab, StartLocation + new Vector3(0f, -i * SlotGap, 0f), Quaternion.identity);
            slot.name = "Slot " + i;
            slot.Init(i);
            SlotStates[i] = SlotState.Unoccupied;
            SlotObjects[i] = slot;
            LoopReference[i] = null;
        }

        spawnedMarker = Instantiate(ProcessingLineMarkerPrefab, SlotObjects[0].transform.position + new Vector3(-2.2f, 0f, 0f), Quaternion.identity);
        spawnedMarker.Reset();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddForLoop(1, 2);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            AddForLoop(2, 5);
        }
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

    public SlotObject GetSlotReference(int index)
    {
        return SlotObjects[index];
    }

    public void MarkSlot(int slotNum)
    {
        spawnedMarker.transform.position = SlotObjects[slotNum].transform.position + new Vector3(-2.2f, 0f, 0f);
        spawnedMarker.Reset();
    }

    public bool AddForEachLoop(int amount)
    {
        for (int i = 0; i < numSlots - amount; i++)
        {
            if (GetSlotAvailability(i, amount + 1))
            {
                DraggerPointer spawnedLoop = Instantiate(ForEachLoopPrefab);
                spawnedLoop.DraggerRef.AdditionalSlotsToReserve = amount;
                spawnedLoop.DraggerRef.AssignSlot(SlotObjects[i]);
                return true;
            }
        }
        return false;
    }

    public bool AddForLoop(int amount, int loopCount)
    {
        for (int i = 0; i < numSlots - amount; i++)
        {
            if (GetSlotAvailability(i, amount + 1))
            {
                DraggerPointer spawnedLoop = Instantiate(ForLoopPrefab);
                spawnedLoop.DraggerRef.AdditionalSlotsToReserve = amount;
                spawnedLoop.DraggerRef.SetLoopCount(loopCount);
                spawnedLoop.DraggerRef.AssignSlot(SlotObjects[i]);
                return true;
            }
        }
        return false;
    }

    public void ResetForEachCounts()
    {
        for (int i = 0; i < numSlots; i++)
        {
            DraggableObject_Loop loopTest = LoopReference[i];
            if (loopTest != null)
            {
                if (loopTest.isForEach)
                {
                    loopTest.ResetForEachCounts();
                }
            }
        }
    }
}
