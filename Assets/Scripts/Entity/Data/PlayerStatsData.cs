using System;

[Serializable]
public class PlayerStatsData
{
    public int MaxHealth { get; set; }
    public int Armor { get; set; }
    public int Damage { get; set; }
    public float AttackDelay { get; set; }
    public int StatPoints { get; set; }
    public int CurrentLevel { get; set; }
}
