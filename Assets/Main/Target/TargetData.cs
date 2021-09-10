using System;
using UnityEngine;

[Serializable]
public struct TargetData
{
    public enum eKind
    {
        Human, Tree, House, Car, Max
    }
    
    public eKind Kind;
    public BaseTarget Prefab;
    public float SuckTime;
    public int Score;
    public float Height;
}
