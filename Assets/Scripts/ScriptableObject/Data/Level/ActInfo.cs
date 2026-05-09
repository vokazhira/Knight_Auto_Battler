using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ActInfo", fileName = "ActInfo", order = 52)]
public class ActInfo : ScriptableObject
{
    public LevelInfo[] Levels;
}

[Serializable]
public class LevelInfo
{
    public Sprite Background;
    public BattleInfo[] Battles;
}

[Serializable]
public class BattleInfo
{
    public EnemyData[] Enemies;
}

