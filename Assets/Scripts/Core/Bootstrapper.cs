using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        var appController = new GameObject("[APP CONTROLLER]").AddComponent<Bootstrapper>();
        DontDestroyOnLoad(appController);

        EventController.Initialize();
        Application.targetFrameRate = 144;
        QualitySettings.vSyncCount = 0;
    }

    void OnApplicationQuit()
    {
        SlotController.Destroy();
        InventoryController.Destroy();
        Debug.Log("Game Closed");
    }
}
