using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluckController : MonoBehaviour
{
    #region Singleton script
    public static CluckController Instance { get; private set; }

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

    public static int ClucksRemaining = 20;

    public static void Process(ItemType item, Actions action)
    {
        Debug.Log(item.ToString() + "is doing " + action.ToString());
    }
}
