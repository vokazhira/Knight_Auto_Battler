using System;

public class EventManager
{
    public static event Action<int> EnemyDied;
    public static event Action PlayerDied;
    public static event Action<float> HealthChanged;

    public static void OnEnemyDied(int amount)
    {
        EnemyDied?.Invoke(amount);
    }

    public static void OnPlayerDied()
    {
        PlayerDied?.Invoke();
    }

    public static void OnHealthChanged(float amount)
    {
        HealthChanged?.Invoke(amount);
    }
}
