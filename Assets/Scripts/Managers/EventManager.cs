using System;
using System.Collections.Generic;

public static class EventManager
{
    private static Dictionary<string, Action<object>> eventDictionary = 
        new Dictionary<string, Action<object>>();

    public static void Subscribe(string eventName, Action<object> callback)
    {
        if (eventDictionary.TryGetValue(eventName, out Action<object> action))
        {
            action += callback;
            eventDictionary[eventName] = action;
        }
        else
        {
            action += callback;
            eventDictionary.Add(eventName, action);
        }
    }

    public static void Unsubscribe(string eventName, Action<object> callback)
    {
        if (eventDictionary.TryGetValue(eventName, out Action<object> action))
        {
            action -= callback;
            eventDictionary[eventName] = action;
        }
    }

    public static void Invoke(string eventName, object arg = null)
    {
        if (eventDictionary.TryGetValue(eventName, out Action<object> action))
        {
            action?.Invoke(arg);
        }
    }
}

public static class GameEvents
{
    public const string PlayerDied = "PlayerDied";
    public const string EnemyDeath = "EnemyDeath";
    public const string ProgressUpdated = "ProgressUpdated";
    public const string StartLevel = "StartLevel";
    public const string LevelCompleted = "LevelCompleted";
    public const string LevelFailed = "LevelFailed";
}
