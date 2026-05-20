using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/StatsData", fileName = "StatsData", order = 52)]
public class StatsIconData : ScriptableObject
{
    public StatInfo[] Stats;
}

[Serializable]
public struct StatInfo
{
    public StatType StatType;
    public Sprite StatIcon;
}

public enum StatType
{
    Damage,
    Health,
    Armor,
    AttackDelay
}