using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MatchType
{
    Solo,
    Competitive
}

[System.Serializable]
public class Arena
{
    public string Name;
    public string Title;
    public string Description;
    public MatchType MatchType;
    public int MaxPlayers;
    public int TrophyUnlock;
    public float TimeSpawn;
    public string[] ItemNames;
}
