using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StringUnityEvent : UnityEvent<string> { }

[System.Serializable]
public class IntUnityEvent : UnityEvent<int> { }

[System.Serializable]
public class FloatUnityEvent : UnityEvent<float> { }

[System.Serializable]
public class TransformUnityEvent : UnityEvent<Transform> { }

public static class EventController
{
    private static Dictionary<string, UnityEvent> eventDictionary;
    private static Dictionary<string, StringUnityEvent> stringEventDictionary;
    private static Dictionary<string, IntUnityEvent> intEventDictionary;
    private static Dictionary<string, FloatUnityEvent> floatEventDictionary;
    private static Dictionary<string, TransformUnityEvent> transformEventDictionary;

    public static void Initialize()
    {
        Debug.Log($"EventController is init ");

        Reset(); //Note: This is needed because domain reload on play is off for now.
        eventDictionary = new Dictionary<string, UnityEvent>();
        stringEventDictionary = new Dictionary<string, StringUnityEvent>();
        intEventDictionary = new Dictionary<string, IntUnityEvent>();
        floatEventDictionary = new Dictionary<string, FloatUnityEvent>();
        transformEventDictionary = new Dictionary<string, TransformUnityEvent>();
    }

    private static void Reset()
    {
        if (eventDictionary == null)
            return;

        foreach (var item in eventDictionary)
            item.Value.RemoveAllListeners();

        foreach (var item in stringEventDictionary)
            item.Value.RemoveAllListeners();

        foreach (var item in intEventDictionary)
            item.Value.RemoveAllListeners();

        foreach (var item in floatEventDictionary)
            item.Value.RemoveAllListeners();

        foreach (var item in transformEventDictionary)
            item.Value.RemoveAllListeners();
    }

    #region Binding
    //Default Listening
    public static void StartListening(string eventName, UnityAction listener)
    {
        if (eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            eventDictionary.Add(eventName, thisEvent);
        }
    }

    //String Listening
    public static void StartListening(string eventName, UnityAction<string> listener)
    {
        StringUnityEvent thisEvent = null;
        if (stringEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new StringUnityEvent();
            thisEvent.AddListener(listener);
            stringEventDictionary.Add(eventName, thisEvent);
        }
    }

    //Int Listening
    public static void StartListening(string eventName, UnityAction<int> listener)
    {
        IntUnityEvent thisEvent = null;
        if (intEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new IntUnityEvent();
            thisEvent.AddListener(listener);
            intEventDictionary.Add(eventName, thisEvent);
        }
    }

    //Float Listening
    public static void StartListening(string eventName, UnityAction<float> listener)
    {
        FloatUnityEvent thisEvent = null;
        if (floatEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new FloatUnityEvent();
            thisEvent.AddListener(listener);
            floatEventDictionary.Add(eventName, thisEvent);
        }
    }

    //Transform Listening
    public static void StartListening(string eventName, UnityAction<Transform> listener)
    {
        TransformUnityEvent thisEvent = null;
        if (transformEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new TransformUnityEvent();
            thisEvent.AddListener(listener);
            transformEventDictionary.Add(eventName, thisEvent);
        }
    }
    #endregion

    #region Unbinding
    //Default Removing
    public static void StopListening(string eventName, UnityAction listener)
    {
        if (eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    //String Removing
    public static void StopListening(string eventName, UnityAction<string> listener)
    {
        if (stringEventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    //Int Removing
    public static void StopListening(string eventName, UnityAction<int> listener)
    {
        if (intEventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    //Float Removing
    public static void StopListening(string eventName, UnityAction<float> listener)
    {
        if (floatEventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    //Transform Removing
    public static void StopListening(string eventName, UnityAction<Transform> listener)
    {
        if (transformEventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    #endregion

    #region Triggers
    //Default trigger
    public static void TriggerEvent(string eventName)
    {
        Global.ProcessEvent(eventName);
        if (eventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke();
        }
    }

    //String trigger
    public static void TriggerEvent(string eventName, string stringVal)
    {
        //UnityEngine.Debug.Log($"Triggering event {eventName} with string value {stringVal}");

        if (stringEventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke(stringVal);
        }
    }

    //Int trigger
    public static void TriggerEvent(string eventName, int intVal)
    {
        Global.ProcessEvent(eventName, intVal);

        //UnityEngine.Debug.Log($"Triggering event {eventName} with int value {intVal}");

        if (intEventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke(intVal);
        }
    }

    //Float trigger
    public static void TriggerEvent(string eventName, float floatVal)
    {
        Global.ProcessEvent(eventName, 0, floatVal);

        if (floatEventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke(floatVal);
        }
    }

    //Transform trigger
    public static void TriggerEvent(string eventName, Transform transformVal)
    {
        if (transformEventDictionary.TryGetValue(eventName, out var thisEvent))
        {
            thisEvent.Invoke(transformVal);
        }
    }

    #endregion
}
