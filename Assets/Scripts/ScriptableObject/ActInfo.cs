using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ActInfo", fileName = "ActInfo", order = 52)]
public class ActInfo : ScriptableObject
{
    public LevelInfo[] Levels;
}

[Serializable]
public class LevelInfo
{
    public GameObject[] Enemies;
    public Sprite BackgroundSprite;
}