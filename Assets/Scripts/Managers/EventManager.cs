using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public static event Action<int> EnemyDied;
    public static event Action PlayerDied;

    public static void OnEnemyDied(int amount)
    {
        EnemyDied?.Invoke(amount);
    }

    public static void OnPlayerDied()
    {
        PlayerDied?.Invoke();
    }
}
